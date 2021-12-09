using Microsoft.EntityFrameworkCore;
using PerRead.Models;

namespace PerRead
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<ArticleModel> Recipes { get; set; }
    }
}
