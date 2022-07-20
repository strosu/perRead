using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerRead.Backend.Migrations
{
    public partial class AddWallets_NoFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Authors_OwnerAuthorId",
                table: "Wallets");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerAuthorId",
                table: "Wallets",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Authors_OwnerAuthorId",
                table: "Wallets",
                column: "OwnerAuthorId",
                principalTable: "Authors",
                principalColumn: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Authors_OwnerAuthorId",
                table: "Wallets");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerAuthorId",
                table: "Wallets",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Authors_OwnerAuthorId",
                table: "Wallets",
                column: "OwnerAuthorId",
                principalTable: "Authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
