using CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Queries.GetBlogs;
using MediatR;

namespace CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Queries.GetBlogById
{
    public record GetBlogByIdQuery : IRequest<BlogVm>
    {
        public int BlogId { get; set; }
    }
}
