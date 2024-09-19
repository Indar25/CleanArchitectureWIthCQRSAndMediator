using CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Command.CreateBlog;
using CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Command.DeleteBlog;
using CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Command.UpdateBlog;
using CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Queries.GetBlogById;
using CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Queries.GetBlogs;
using CleanArchitectureWIthCQRSAndMediator.Model;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CleanArchitectureWIthCQRSAndMediator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : APIBaseController
    {
        private readonly ILogger<BlogController> _logger;
        private readonly IProducer<string, string> _producer;
        private readonly IConsumer<string, string> _consumer;
        // Inject the Kafka producer directly
        public BlogController(IProducer<string, string> producer,
                            ILogger<BlogController> logger,
                            IConsumer<string, string> consumer)
        {
            _producer = producer;
            _logger = logger;
            _consumer = consumer;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var blobs = await Mediator.Send(new GetBlogQuery());
            _= ProduceAsync(blobs, "topic_userDetails");
            return Ok(blobs);
        }
        private  List<BlogVm> ConsumeMessages(string topic, CancellationToken cancellationToken)
        {
            _consumer.Subscribe(topic);
            _logger.LogInformation($"Subscribed to topic {topic}");

            var blogs = new List<BlogVm>();

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        // Consume messages without a fixed timeout
                        var consumeResult = _consumer.Consume(cancellationToken);  // Token-based consume

                        if (consumeResult != null && !string.IsNullOrEmpty(consumeResult.Message.Value))
                        {
                            _logger.LogInformation($"Consumed event from topic {topic}: key = {consumeResult.Message.Key}, value = {consumeResult.Message.Value}");

                            // Deserialize the message content
                            var consumedBlogs = System.Text.Json.JsonSerializer.Deserialize<List<BlogVm>>(consumeResult.Message.Value);

                            if (consumedBlogs != null)
                            {
                                blogs.AddRange(consumedBlogs);
                            }

                            // Commit the offset after successfully processing the message
                            _consumer.Commit(consumeResult);
                        }
                        else
                        {
                            _logger.LogWarning("Received an empty or null message.");
                        }
                    }
                    catch (ConsumeException e)
                    {
                        _logger.LogError($"Error occurred while consuming: {e.Error.Reason}");
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Error deserializing message: {jsonEx.Message}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Consumer operation was cancelled.");
            }
            finally
            {
                _consumer.Close();
                _logger.LogInformation("Consumer closed.");
            }

            return blogs;
        }


        private async Task ProduceAsync(List<BlogVm> blogs, string topic)
        {
            try
            {
                // Convert blog data to JSON format
                var value = System.Text.Json.JsonSerializer.Serialize(blogs);

                // Producing message asynchronously
                var deliveryResult = await _producer.ProduceAsync(topic, new Message<string, string>
                {
                    Key = "key",
                    Value = value
                });

                _logger.LogInformation($"Produced event to topic {topic}: key = {deliveryResult.Key}, value = {deliveryResult.Value}");
            }
            catch (ProduceException<string, string> ex)
            {
                _logger.LogError($"Failed to deliver message to {topic}: {ex.Error.Reason}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error occurred while producing message: {ex.Message}");
            }
        }
        [HttpPost("consume")]
        public async Task<IActionResult> Consume(string topic)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            // Run the consumer on a background thread
            var blogs = ConsumeMessages(topic, cancellationTokenSource.Token);

            return Ok(await Task.Run(()=> blogs));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var blog = await Mediator.Send(new GetBlogByIdQuery() { BlogId = id });

            if (blog == null)
            {
                return NotFound();
            }
            return Ok(blog);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogCommand command)
        {
            var blog = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = blog.Id }, blog);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBlogCommand command)
        {
            if (command.Id != id)
            {
                return BadRequest();
            }
            int blog = await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int blog = await Mediator.Send(new DeleteBlogCommand() { BogId = id });

            return NoContent();
        }


    }
}
