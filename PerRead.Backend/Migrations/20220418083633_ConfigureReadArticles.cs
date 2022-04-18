using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerRead.Backend.Migrations
{
    public partial class ConfigureReadArticles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnlockedArticles");

            migrationBuilder.CreateTable(
                name: "ArticleUnlock",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorId = table.Column<string>(type: "TEXT", nullable: false),
                    ArticleId = table.Column<int>(type: "INTEGER", nullable: false),
                    AquisitionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AquisitionPrice = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleUnlock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleUnlock_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleUnlock_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleUnlock_ArticleId",
                table: "ArticleUnlock",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleUnlock_AuthorId",
                table: "ArticleUnlock",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleUnlock");

            migrationBuilder.CreateTable(
                name: "UnlockedArticles",
                columns: table => new
                {
                    UnlockedArticlesArticleId = table.Column<int>(type: "INTEGER", nullable: false),
                    UnlockedAuthorId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnlockedArticles", x => new { x.UnlockedArticlesArticleId, x.UnlockedAuthorId });
                    table.ForeignKey(
                        name: "FK_UnlockedArticles_Articles_UnlockedArticlesArticleId",
                        column: x => x.UnlockedArticlesArticleId,
                        principalTable: "Articles",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnlockedArticles_Authors_UnlockedAuthorId",
                        column: x => x.UnlockedAuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnlockedArticles_UnlockedAuthorId",
                table: "UnlockedArticles",
                column: "UnlockedAuthorId");
        }
    }
}
