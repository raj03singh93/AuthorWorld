using AuthorWorld.Domain.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace AuthorWorld.Domain.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Books> Books { get; set; }
    }
}
