using AutoMapper;
using CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Queries.GetBlogs;
using CleanArchitectureWIthCQRSAndMediator.Domain.Entity;
using CleanArchitectureWIthCQRSAndMediator.Domain.Repository;
using MediatR;

namespace CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Command.CreateBlog
{
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, BlogVm>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        public CreateBlogCommandHandler(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }
        public async Task<BlogVm> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var entity = new Blog()
            {
                Author = request.Author,
                Description = request.Description,
                Name = request.Name,
            };
            var blob = await _blogRepository.CreateAsync(entity);

            return _mapper.Map<BlogVm>(blob);
        }
    }
}
