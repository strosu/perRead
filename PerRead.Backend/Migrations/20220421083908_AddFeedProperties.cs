using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerRead.Backend.Migrations
{
    public partial class AddFeedProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequireConfirmationAbove",
                table: "Feeds",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ShowArticlesAboveConfirmationLimit",
                table: "Feeds",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowFreeArticles",
                table: "Feeds",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowUnaffordableArticles",
                table: "Feeds",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequireConfirmationAbove",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "ShowArticlesAboveConfirmationLimit",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "ShowFreeArticles",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "ShowUnaffordableArticles",
                table: "Feeds");
        }
    }
}
