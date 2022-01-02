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

        //public DbSet<ArticleTag> ArticleTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Article>()
                .HasMany(p => p.Tags)
                .WithMany(p => p.Articles)
                .UsingEntity(j => j.ToTable("ArticleTag"));

            modelBuilder.Entity<ArticleAuthor>()
                .HasKey(x => new { x.ArticleId, x.AuthorId });

        }
    }
}
