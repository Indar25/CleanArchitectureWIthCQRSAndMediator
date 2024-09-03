using AutoMapper;
using CleanArchitectureWIthCQRSAndMediator.Domain.Repository;
using MediatR;

namespace CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Queries.GetBlogs
{
    public class GetQueryBlogHandler : IRequestHandler<GetBlogQuery, List<BlogVm>>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        public GetQueryBlogHandler(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }
        public async Task<List<BlogVm>> Handle(GetBlogQuery request, CancellationToken cancellationToken)
        {
            var blogs = await _blogRepository.GetListAsync();

            return _mapper.Map<List<BlogVm>>(blogs);
        }
    }
}
