using MediatR;

namespace CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Command.UpdateBlog
{
    public class UpdateBlogCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Author { get; set; }
    }
}
