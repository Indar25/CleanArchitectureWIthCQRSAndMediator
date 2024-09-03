using CleanArchitectureWIthCQRSAndMediator.Domain.Entity;
using CleanArchitectureWIthCQRSAndMediator.Domain.Repository;
using CleanArchitectureWIthCQRSAndMediator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureWIthCQRSAndMediator.Infrastructure.Repository
{
    public class BlogRepository : IBlogRepository
    {
        public BlogRepository(BlogDBContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        private readonly BlogDBContext _dbContext;

        public async Task<Blog> CreateAsync(Blog blog)
        {
            await _dbContext.AddAsync(blog);
            await _dbContext.SaveChangesAsync();

            return blog;
        }

        public async Task<int> DeleteAsync(int id)
        {
            await _dbContext.Blogs.Where(x => x.Id == id).ExecuteDeleteAsync();
            return id;
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            var blog = await _dbContext.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return blog;
        }

        public async Task<List<Blog>> GetListAsync()
        {
            var blogs = await _dbContext.Blogs.ToListAsync();
            return blogs;
        }

        public async Task<int> UpdateAsync(int id, Blog blog)
        {
            await _dbContext.Blogs.Where(x => x.Id == id).ExecuteUpdateAsync(
                  set => set.SetProperty(b => b.Name, blog.Name)
                            .SetProperty(b => b.Description, blog.Description)
                            .SetProperty(b => b.Author, blog.Author)
                );
            return id;
        }
    }
}
