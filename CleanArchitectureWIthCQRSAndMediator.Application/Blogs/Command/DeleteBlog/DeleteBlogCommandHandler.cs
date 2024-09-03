using AutoMapper;
using CleanArchitectureWIthCQRSAndMediator.Domain.Entity;
using CleanArchitectureWIthCQRSAndMediator.Domain.Repository;
using MediatR;

namespace CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Command.DeleteBlog
{
    internal class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;
        public DeleteBlogCommandHandler(IMapper mapper, IBlogRepository blogRepository)
        {
            _mapper = mapper;
            _blogRepository = blogRepository;
        }

        public async Task<int> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
        {
            var result = await _blogRepository.DeleteAsync(request.BogId);
            return result;
        }
    }
}
