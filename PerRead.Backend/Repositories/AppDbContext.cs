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
        
        public DbSet<ArticleTag> ArticleTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleTag>()
                .HasKey(bc => new { bc.ArticleId, bc.TagId});

            modelBuilder.Entity<ArticleTag>()
                .HasOne(bc => bc.Article)
                .WithMany(a => a.Tags)
                .HasForeignKey(bc => bc.TagId);

            modelBuilder.Entity<ArticleTag>()
                .HasOne(bc => bc.Tag)
                .WithMany(c => c.Articles)
                .HasForeignKey(bc => bc.TagId);
        }
    }
}
