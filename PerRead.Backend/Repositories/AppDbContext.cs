using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models;

namespace PerRead.Backend.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Author> Authors { get; set; }
        
        public DbSet<Tag> Tags { get; set; }
    }
}
