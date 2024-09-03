using CleanArchitectureWIthCQRSAndMediator.Domain.Entity;

namespace CleanArchitectureWIthCQRSAndMediator.Domain.Repository
{
    public interface IBlogRepository
    {
        Task<List<Blog>> GetListAsync();
        Task<Blog> GetByIdAsync(int id);
        Task<Blog> CreateAsync(Blog blog);
        Task<int> UpdateAsync(int id, Blog blog);
        Task<int> DeleteAsync(int id);
    }
}
