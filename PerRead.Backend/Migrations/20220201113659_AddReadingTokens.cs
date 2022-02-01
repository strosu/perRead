using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerRead.Backend.Migrations
{
    public partial class AddReadingTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReadingTokens",
                table: "Authors",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadingTokens",
                table: "Authors");
        }
    }
}
