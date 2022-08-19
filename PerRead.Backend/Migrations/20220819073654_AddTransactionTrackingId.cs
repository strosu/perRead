using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerRead.Backend.Migrations
{
    public partial class AddTransactionTrackingId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransactionTrackingId",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionTrackingId",
                table: "Transactions");
        }
    }
}
