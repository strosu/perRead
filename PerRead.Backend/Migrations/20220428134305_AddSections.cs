using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerRead.Backend.Migrations
{
    public partial class AddSections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedAuthors");

            migrationBuilder.AlterColumn<string>(
                name: "ArticleId",
                table: "ArticleUnlock",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "ArticlesArticleId",
                table: "ArticleTag",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "ArticleId",
                table: "Articles",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<string>(
                name: "ArticleId",
                table: "ArticleAuthor",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    SectionId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AuthorId = table.Column<string>(type: "TEXT", nullable: false),
                    FeedId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.SectionId);
                    table.ForeignKey(
                        name: "FK_Sections_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sections_Feeds_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feeds",
                        principalColumn: "FeedId");
                });

            migrationBuilder.CreateTable(
                name: "SectionArticle",
                columns: table => new
                {
                    SectionId = table.Column<string>(type: "TEXT", nullable: false),
                    ArticleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionArticle", x => new { x.ArticleId, x.SectionId });
                    table.ForeignKey(
                        name: "FK_SectionArticle_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SectionArticle_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "SectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SectionArticle_SectionId",
                table: "SectionArticle",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_AuthorId",
                table: "Sections",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_FeedId",
                table: "Sections",
                column: "FeedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SectionArticle");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "ArticleUnlock",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "ArticlesArticleId",
                table: "ArticleTag",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "Articles",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "ArticleAuthor",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "FeedAuthors",
                columns: table => new
                {
                    SubscribedAuthorsAuthorId = table.Column<string>(type: "TEXT", nullable: false),
                    SubscribedFeedsFeedId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedAuthors", x => new { x.SubscribedAuthorsAuthorId, x.SubscribedFeedsFeedId });
                    table.ForeignKey(
                        name: "FK_FeedAuthors_Authors_SubscribedAuthorsAuthorId",
                        column: x => x.SubscribedAuthorsAuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedAuthors_Feeds_SubscribedFeedsFeedId",
                        column: x => x.SubscribedFeedsFeedId,
                        principalTable: "Feeds",
                        principalColumn: "FeedId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedAuthors_SubscribedFeedsFeedId",
                table: "FeedAuthors",
                column: "SubscribedFeedsFeedId");
        }
    }
}
