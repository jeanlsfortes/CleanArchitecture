using CleanArchitecture.Domain.Enitites;
using CleanArchitecture.Domain.Interface;
using CleanArchitecture.Infrastructure.Common;
using CleanArchitecture.Infrastructure.Data;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        public BlogRepository(BlogDbContext context) : base(context)
        {
        }
    }
}
