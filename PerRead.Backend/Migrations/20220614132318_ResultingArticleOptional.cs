using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerRead.Backend.Migrations
{
    public partial class ResultingArticleOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Articles_ResultingArticleArticleId",
                table: "Requests");

            migrationBuilder.AlterColumn<string>(
                name: "ResultingArticleArticleId",
                table: "Requests",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Articles_ResultingArticleArticleId",
                table: "Requests",
                column: "ResultingArticleArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Articles_ResultingArticleArticleId",
                table: "Requests");

            migrationBuilder.AlterColumn<string>(
                name: "ResultingArticleArticleId",
                table: "Requests",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Articles_ResultingArticleArticleId",
                table: "Requests",
                column: "ResultingArticleArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
