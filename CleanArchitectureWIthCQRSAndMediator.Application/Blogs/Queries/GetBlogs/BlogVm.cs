using AutoMapper;
using CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Common.Mappings;
using CleanArchitectureWIthCQRSAndMediator.Domain.Entity;
using System.Reflection.Metadata;

namespace CleanArchitectureWIthCQRSAndMediator.Application.Blogs.Queries.GetBlogs
{
    public class BlogVm : IMapFrom<Blob>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Author { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Blog, BlogVm>();
        }
    }
}
