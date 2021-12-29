using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerRead.Backend.Migrations
{
    public partial class AddArticleContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Articles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Articles");
        }
    }
}
