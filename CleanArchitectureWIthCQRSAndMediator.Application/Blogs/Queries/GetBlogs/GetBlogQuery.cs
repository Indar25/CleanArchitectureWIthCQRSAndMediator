using MediatR;

namespace CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Queries.GetBlogs
{
    public record GetBlogQuery : IRequest<List<BlogVm>>;
}
