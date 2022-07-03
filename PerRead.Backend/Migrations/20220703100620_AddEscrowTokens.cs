using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerRead.Backend.Migrations
{
    public partial class AddEscrowTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EscrowTokens",
                table: "Authors",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EscrowTokens",
                table: "Authors");
        }
    }
}
