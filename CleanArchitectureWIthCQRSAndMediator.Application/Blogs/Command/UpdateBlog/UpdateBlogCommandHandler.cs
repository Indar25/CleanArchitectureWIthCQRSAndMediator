using AutoMapper;
using CleanArchitectureWIthCQRSAndMediator.Domain.Entity;
using CleanArchitectureWIthCQRSAndMediator.Domain.Repository;
using MediatR;

namespace CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Command.UpdateBlog
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;
        public UpdateBlogCommandHandler(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var result = await _blogRepository.UpdateAsync(request.Id, new Blog
            {
                Id = request.Id,
                Author = request.Author,
                Description = request.Description,
                Name = request.Name
            });

            return result;
        }
    }
}
