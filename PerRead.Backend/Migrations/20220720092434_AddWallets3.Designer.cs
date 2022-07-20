﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PerRead.Backend.Repositories;

#nullable disable

namespace PerRead.Backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220720092434_AddWallets3")]
    partial class AddWallets3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("ArticleTag", b =>
                {
                    b.Property<string>("ArticlesArticleId")
                        .HasColumnType("TEXT");

                    b.Property<int>("TagsTagId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ArticlesArticleId", "TagsTagId");

                    b.HasIndex("TagsTagId");

                    b.ToTable("ArticleTag", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PerRead.Backend.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.Article", b =>
                {
                    b.Property<string>("ArticleId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<uint>("Price")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ArticleId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.ArticleAuthor", b =>
                {
                    b.Property<string>("ArticleId")
                        .HasColumnType("TEXT");

                    b.Property<string>("AuthorId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.HasKey("ArticleId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("ArticleAuthor");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.ArticleRequest", b =>
                {
                    b.Property<string>("ArticleRequestId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("InitiatorAuthorId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<uint>("PercentForledgers")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PostPublishState")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RequestState")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ResultingArticleArticleId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TargetAuthorAuthorId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ArticleRequestId");

                    b.HasIndex("InitiatorAuthorId");

                    b.HasIndex("ResultingArticleArticleId");

                    b.HasIndex("TargetAuthorAuthorId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.ArticleUnlock", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AquisitionDate")
                        .HasColumnType("TEXT");

                    b.Property<uint>("AquisitionPrice")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ArticleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("AuthorId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("AuthorId");

                    b.ToTable("ArticleUnlock");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.Author", b =>
                {
                    b.Property<string>("AuthorId")
                        .HasColumnType("TEXT");

                    b.Property<string>("About")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EscrowWalletId")
                        .HasColumnType("TEXT");

                    b.Property<string>("MainWalletId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PopularityRank")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProfileImageUri")
                        .HasColumnType("TEXT");

                    b.Property<int>("PublishedArticleCount")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("RequireConfirmationAbove")
                        .HasColumnType("INTEGER");

                    b.HasKey("AuthorId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.Feed", b =>
                {
                    b.Property<string>("FeedId")
                        .HasColumnType("TEXT");

                    b.Property<string>("FeedName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OwnerAuthorId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RequireConfirmationAbove")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ShowArticlesAboveConfirmationLimit")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ShowFreeArticles")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ShowUnaffordableArticles")
                        .HasColumnType("INTEGER");

                    b.HasKey("FeedId");

                    b.HasIndex("OwnerAuthorId");

                    b.ToTable("Feeds");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.PaymentTransaction", b =>
                {
                    b.Property<string>("PaymentTransactionId")
                        .HasColumnType("TEXT");

                    b.Property<string>("DestinationWalletId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SourceWalletId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("TokenAmount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TransactionType")
                        .HasColumnType("INTEGER");

                    b.HasKey("PaymentTransactionId");

                    b.HasIndex("DestinationWalletId");

                    b.HasIndex("SourceWalletId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.RequestPledge", b =>
                {
                    b.Property<string>("RequestPledgeId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("ParentRequestArticleRequestId")
                        .HasColumnType("TEXT");

                    b.Property<string>("PledgerAuthorId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<uint>("TokensOnAccept")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("TotalTokenSum")
                        .HasColumnType("INTEGER");

                    b.HasKey("RequestPledgeId");

                    b.HasIndex("ParentRequestArticleRequestId");

                    b.HasIndex("PledgerAuthorId");

                    b.ToTable("Pledges");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.Section", b =>
                {
                    b.Property<string>("SectionId")
                        .HasColumnType("TEXT");

                    b.Property<string>("AuthorId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("SectionId");

                    b.HasIndex("AuthorId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.SectionArticle", b =>
                {
                    b.Property<string>("ArticleId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SectionId")
                        .HasColumnType("TEXT");

                    b.HasKey("ArticleId", "SectionId");

                    b.HasIndex("SectionId");

                    b.ToTable("SectionArticle");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FirstUsage")
                        .HasColumnType("TEXT");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.Wallet", b =>
                {
                    b.Property<string>("WalledId")
                        .HasColumnType("TEXT");

                    b.Property<string>("OwnerAuthorId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("TokenAmount")
                        .HasColumnType("INTEGER");

                    b.HasKey("WalledId");

                    b.HasIndex("OwnerAuthorId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("SectionFeedMapping", b =>
                {
                    b.Property<string>("SectionId")
                        .HasColumnType("TEXT");

                    b.Property<string>("FeedId")
                        .HasColumnType("TEXT");

                    b.HasKey("SectionId", "FeedId");

                    b.HasIndex("FeedId");

                    b.ToTable("SectionFeedMapping");
                });

            modelBuilder.Entity("ArticleTag", b =>
                {
                    b.HasOne("PerRead.Backend.Models.BackEnd.Article", null)
                        .WithMany()
                        .HasForeignKey("ArticlesArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PerRead.Backend.Models.BackEnd.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PerRead.Backend.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PerRead.Backend.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PerRead.Backend.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.ArticleAuthor", b =>
                {
                    b.HasOne("PerRead.Backend.Models.BackEnd.Article", "Article")
                        .WithMany("ArticleAuthors")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PerRead.Backend.Models.BackEnd.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.ArticleRequest", b =>
                {
                    b.HasOne("PerRead.Backend.Models.BackEnd.Author", "Initiator")
                        .WithMany()
                        .HasForeignKey("InitiatorAuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PerRead.Backend.Models.BackEnd.Article", "ResultingArticle")
                        .WithMany()
                        .HasForeignKey("ResultingArticleArticleId");

                    b.HasOne("PerRead.Backend.Models.BackEnd.Author", "TargetAuthor")
                        .WithMany("Requests")
                        .HasForeignKey("TargetAuthorAuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Initiator");

                    b.Navigation("ResultingArticle");

                    b.Navigation("TargetAuthor");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.ArticleUnlock", b =>
                {
                    b.HasOne("PerRead.Backend.Models.BackEnd.Article", "Article")
                        .WithMany()
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PerRead.Backend.Models.BackEnd.Author", "Author")
                        .WithMany("UnlockedArticles")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.Feed", b =>
                {
                    b.HasOne("PerRead.Backend.Models.BackEnd.Author", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerAuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.PaymentTransaction", b =>
                {
                    b.HasOne("PerRead.Backend.Models.BackEnd.Wallet", "DestinationWallet")
                        .WithMany("IncomingTransactions")
                        .HasForeignKey("DestinationWalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PerRead.Backend.Models.BackEnd.Wallet", "SourceWallet")
                        .WithMany("OutgoingTransactions")
                        .HasForeignKey("SourceWalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DestinationWallet");

                    b.Navigation("SourceWallet");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.RequestPledge", b =>
                {
                    b.HasOne("PerRead.Backend.Models.BackEnd.ArticleRequest", "ParentRequest")
                        .WithMany("Pledges")
                        .HasForeignKey("ParentRequestArticleRequestId");

                    b.HasOne("PerRead.Backend.Models.BackEnd.Author", "Pledger")
                        .WithMany("Pledges")
                        .HasForeignKey("PledgerAuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentRequest");

                    b.Navigation("Pledger");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.Section", b =>
                {
                    b.HasOne("PerRead.Backend.Models.BackEnd.Author", "Author")
                        .WithMany("PublishSections")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.SectionArticle", b =>
                {
                    b.HasOne("PerRead.Backend.Models.BackEnd.Article", "Article")
                        .WithMany("Sections")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PerRead.Backend.Models.BackEnd.Section", "Section")
                        .WithMany("Articles")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.Wallet", b =>
                {
                    b.HasOne("PerRead.Backend.Models.BackEnd.Author", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerAuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("SectionFeedMapping", b =>
                {
                    b.HasOne("PerRead.Backend.Models.BackEnd.Feed", "Feed")
                        .WithMany("SubscribedSections")
                        .HasForeignKey("FeedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PerRead.Backend.Models.BackEnd.Section", "Section")
                        .WithMany("SubscribedFeeds")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Feed");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.Article", b =>
                {
                    b.Navigation("ArticleAuthors");

                    b.Navigation("Sections");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.ArticleRequest", b =>
                {
                    b.Navigation("Pledges");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.Author", b =>
                {
                    b.Navigation("Pledges");

                    b.Navigation("PublishSections");

                    b.Navigation("Requests");

                    b.Navigation("UnlockedArticles");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.Feed", b =>
                {
                    b.Navigation("SubscribedSections");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.Section", b =>
                {
                    b.Navigation("Articles");

                    b.Navigation("SubscribedFeeds");
                });

            modelBuilder.Entity("PerRead.Backend.Models.BackEnd.Wallet", b =>
                {
                    b.Navigation("IncomingTransactions");

                    b.Navigation("OutgoingTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
