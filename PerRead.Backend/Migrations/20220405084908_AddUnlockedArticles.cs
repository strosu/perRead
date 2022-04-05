using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerRead.Backend.Migrations
{
    public partial class AddUnlockedArticles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnlockedArticles");
        }
    }
}
