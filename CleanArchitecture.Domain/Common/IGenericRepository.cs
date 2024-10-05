namespace CleanArchitecture.Domain.Common
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> UpdateAsync(T entity);
    }
}