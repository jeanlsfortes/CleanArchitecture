using CleanArchitecture.Domain.Enitites;
using Microsoft.EntityFrameworkCore;


namespace CleanArchitecture.Infrastructure.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Blog> Blogs { get; set; }
    }
}
