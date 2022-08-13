using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerRead.Backend.Migrations
{
    public partial class MergeOwnershipAndAuthors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleAuthor");

            migrationBuilder.AddColumn<bool>(
                name: "CanBeRemoved",
                table: "ArticleOwners",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUserFacing",
                table: "ArticleOwners",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleOwners_AuthorId",
                table: "ArticleOwners",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleOwners_Articles_ArticleId",
                table: "ArticleOwners",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleOwners_Authors_AuthorId",
                table: "ArticleOwners",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleOwners_Articles_ArticleId",
                table: "ArticleOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleOwners_Authors_AuthorId",
                table: "ArticleOwners");

            migrationBuilder.DropIndex(
                name: "IX_ArticleOwners_AuthorId",
                table: "ArticleOwners");

            migrationBuilder.DropColumn(
                name: "CanBeRemoved",
                table: "ArticleOwners");

            migrationBuilder.DropColumn(
                name: "IsUserFacing",
                table: "ArticleOwners");

            migrationBuilder.CreateTable(
                name: "ArticleAuthor",
                columns: table => new
                {
                    ArticleId = table.Column<string>(type: "TEXT", nullable: false),
                    AuthorId = table.Column<string>(type: "TEXT", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleAuthor", x => new { x.ArticleId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_ArticleAuthor_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleAuthor_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleAuthor_AuthorId",
                table: "ArticleAuthor",
                column: "AuthorId");
        }
    }
}
