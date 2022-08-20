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

        public DbSet<Section> Sections { get; set; }

        public DbSet<ArticleRequest> Requests { get; set; }

        public DbSet<RequestPledge> Pledges { get; set; }

        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<PaymentTransaction> Transactions { get; set; }

        public DbSet<ArticleReview> Reviews { get; set; }

        //public DbSet<ArticleOwner> ArticleOwners { get; set; }

        //public DbSet<ArticleTag> ArticleTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Article>()
                .HasMany(p => p.Tags)
                .WithMany(p => p.Articles)
                .UsingEntity(j => j.ToTable("ArticleTag"));

            modelBuilder.Entity<ArticleOwner>()
                .HasKey(x => new { x.ArticleId, x.AuthorId });

            modelBuilder.Entity<Author>()
                .HasMany(x => x.Requests).WithOne(x => x.TargetAuthor);

            modelBuilder.Entity<Author>()
                .HasMany(x => x.Pledges).WithOne(x => x.Pledger);

            modelBuilder.Entity<SectionArticle>()
                .HasKey(x => new { x.ArticleId, x.SectionId });

            modelBuilder.Entity<SectionFeedMapping>()
                .HasKey(x => new { x.SectionId, x.FeedId });

            modelBuilder.Entity<Wallet>()
                .HasKey(x => x.WalledId);

            //modelBuilder.Entity<Author>()
            //    .HasMany(p => p.UnlockedArticles)
            //    .WithMany(p => p.Unlocked)
            //    .UsingEntity(j => j.ToTable("UnlockedArticles"));

            //modelBuilder.Entity<Feed>()
            //    .HasMany(p => p.SubscribedSections)
            //    .WithMany(p => p.SubscribedFeeds)
            //    .UsingEntity(j => j.ToTable("FeedAuthors"));

            modelBuilder.Entity<Feed>()
                .HasOne(p => p.Owner);

            modelBuilder.Entity<ArticleRequest>()
                .HasOne(p => p.Initiator);

            modelBuilder.Entity<ArticleRequest>()
                .HasOne(p => p.TargetAuthor);

            modelBuilder.Entity<Author>().HasOne(x => x.MainWallet)
                .WithOne()
                //.HasForeignKey<Author>(x => x.MainWalletId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Author>().HasOne(x => x.EscrowWallet)
                .WithOne()
                //.HasForeignKey<Author>(x => x.EscrowWalletId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PaymentTransaction>()
                .HasOne(g => g.SourceWallet)
                .WithMany(t => t.OutgoingTransactions)
                .HasForeignKey(t => t.SourceWalletId)
                .HasPrincipalKey(t => t.WalledId);

            modelBuilder.Entity<PaymentTransaction>()
                .HasOne(g => g.DestinationWallet)
                .WithMany(t => t.IncomingTransactions)
                .HasForeignKey(t => t.DestinationWalletId)
                .HasPrincipalKey(t => t.WalledId);

            //modelBuilder.Entity<ArticleRequest>()
            //    .HasOne(x => x.ResultingArticle)
            //    .WithOne(x => x.SourceRequest)
            //    .HasForeignKey<Article>(x => x.ArticleId)
            //    .IsRequired(false);

            //modelBuilder.Entity<ArticleReview>()
            //    .HasOne(x => x.ArticleUnlock)
            //    .WithOne(x => x.Review)
            //    .HasForeignKey<ArticleUnlock>(x => x.Id)
            //    .IsRequired(false);

            //modelBuilder.Entity<PaymentTransaction>()
            //    .HasOne(x => x.SourceWallet)
            //    .WithOne()
            //    .HasForeignKey<PaymentTransaction>(x => x.SourceWalletId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<PaymentTransaction>()
            //    .HasOne(x => x.DestinationWallet)
            //    .WithOne()
            //    .HasForeignKey<PaymentTransaction>(x => x.DestinationWalletId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
