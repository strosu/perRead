using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models;
using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Repositories
{
    public class AppDbContext : IdentityUserContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Feed> Feeds { get; set; }

        //public DbSet<ArticleTag> ArticleTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Article>()
                .HasMany(p => p.Tags)
                .WithMany(p => p.Articles)
                .UsingEntity(j => j.ToTable("ArticleTag"));

            modelBuilder.Entity<ArticleAuthor>()
                .HasKey(x => new { x.ArticleId, x.AuthorId });

            modelBuilder.Entity<Author>()
                .HasMany(p => p.UnlockedArticles)
                .WithMany(p => p.Unlocked)
                .UsingEntity(j => j.ToTable("UnlockedArticles"));

            modelBuilder.Entity<Feed>()
                .HasMany(p => p.SubscribedAuthors)
                .WithMany(p => p.SubscribedFeeds)
                .UsingEntity(j => j.ToTable("FeedAuthors"));

            modelBuilder.Entity<Feed>()
                .HasOne(p => p.Owner);
        }
    }
}
