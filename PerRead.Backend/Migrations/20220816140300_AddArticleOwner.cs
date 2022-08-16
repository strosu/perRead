using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerRead.Backend.Migrations
{
    public partial class AddArticleOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleOwners_Articles_ArticleId",
                table: "ArticleOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleOwners_Authors_AuthorId",
                table: "ArticleOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleOwners",
                table: "ArticleOwners");

            migrationBuilder.RenameTable(
                name: "ArticleOwners",
                newName: "ArticleOwner");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleOwners_AuthorId",
                table: "ArticleOwner",
                newName: "IX_ArticleOwner_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleOwner",
                table: "ArticleOwner",
                columns: new[] { "ArticleId", "AuthorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleOwner_Articles_ArticleId",
                table: "ArticleOwner",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleOwner_Authors_AuthorId",
                table: "ArticleOwner",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleOwner_Articles_ArticleId",
                table: "ArticleOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleOwner_Authors_AuthorId",
                table: "ArticleOwner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleOwner",
                table: "ArticleOwner");

            migrationBuilder.RenameTable(
                name: "ArticleOwner",
                newName: "ArticleOwners");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleOwner_AuthorId",
                table: "ArticleOwners",
                newName: "IX_ArticleOwners_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleOwners",
                table: "ArticleOwners",
                columns: new[] { "ArticleId", "AuthorId" });

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
    }
}
