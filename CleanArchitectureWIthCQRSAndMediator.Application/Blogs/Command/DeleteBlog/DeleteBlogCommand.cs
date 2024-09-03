using MediatR;

namespace CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Command.DeleteBlog
{
    public class DeleteBlogCommand : IRequest<int>
    {
        public int BogId { get; set; }
    }
}
