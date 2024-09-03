using CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Queries.GetBlogs;
using MediatR;

namespace CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Command.CreateBlog
{
    public class CreateBlogCommand : IRequest<BlogVm>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string Author { get; set; }

    }
}
