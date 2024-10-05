using CleanArchitecture.Domain.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interface
{
    public interface IBlogRepository
    {
        Task<List<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(Guid id);
        Task<Blog> CreateAsync(Blog blog);
        Task<Guid> UpdateAsync(Guid id, Blog blog);
        Task<Guid> DeleteAsync(Guid id);
    }
}
