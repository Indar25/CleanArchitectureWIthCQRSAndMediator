using CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Command.CreateBlog;
using CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Command.DeleteBlog;
using CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Command.UpdateBlog;
using CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Queries.GetBlogById;
using CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Queries.GetBlogs;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureWIthCQRSAndMediator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : APIBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var blobs = await Mediator.Send(new GetBlogQuery());
            return Ok(blobs);
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
        public async Task<IActionResult> Update(int id , UpdateBlogCommand command)
        {
            if (command.Id == id)
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
