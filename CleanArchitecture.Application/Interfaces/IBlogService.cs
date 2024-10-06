using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Domain.Enitites;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IBlogService
    {
        Task<Blog> CreateAsync(BlogCreateDto entity);
        Task<bool> DeleteAsync(Guid id);
        Task<List<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(Guid id);
        Task<Blog> UpdateAsync(BlogUpdateDto entity);
    }
}
