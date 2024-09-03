using AutoMapper;
using CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Queries.GetBlogs;
using CleanArchitectureWIthCQRSAndMediator.Domain.Repository;
using MediatR;

namespace CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Queries.GetBlogById
{
    public class GetBlogByIdQueryHandler : IRequestHandler<GetBlogByIdQuery, BlogVm>
    {
        public GetBlogByIdQueryHandler(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }
        private readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;

        public async Task<BlogVm> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetByIdAsync(request.BlogId);

            return _mapper.Map<BlogVm>(blog);
        }
    }
}
