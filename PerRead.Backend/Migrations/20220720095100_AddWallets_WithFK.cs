using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerRead.Backend.Migrations
{
    public partial class AddWallets_WithFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MainWalletId",
                table: "Authors",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EscrowWalletId",
                table: "Authors",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_EscrowWalletId",
                table: "Authors",
                column: "EscrowWalletId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_MainWalletId",
                table: "Authors",
                column: "MainWalletId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Wallets_EscrowWalletId",
                table: "Authors",
                column: "EscrowWalletId",
                principalTable: "Wallets",
                principalColumn: "WalledId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Wallets_MainWalletId",
                table: "Authors",
                column: "MainWalletId",
                principalTable: "Wallets",
                principalColumn: "WalledId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Wallets_EscrowWalletId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Wallets_MainWalletId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_EscrowWalletId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_MainWalletId",
                table: "Authors");

            migrationBuilder.AlterColumn<string>(
                name: "MainWalletId",
                table: "Authors",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "EscrowWalletId",
                table: "Authors",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
