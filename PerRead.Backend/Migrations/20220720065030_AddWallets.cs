using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerRead.Backend.Migrations
{
    public partial class AddWallets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EscrowTokens",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "ReadingTokens",
                table: "Authors");

            migrationBuilder.AddColumn<string>(
                name: "EscrowWalletId",
                table: "Authors",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MainWalletId",
                table: "Authors",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    WalledId = table.Column<string>(type: "TEXT", nullable: false),
                    OwnerAuthorId = table.Column<string>(type: "TEXT", nullable: false),
                    TokenAmount = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.WalledId);
                    table.ForeignKey(
                        name: "FK_Wallets_Authors_OwnerAuthorId",
                        column: x => x.OwnerAuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    PaymentTransactionId = table.Column<string>(type: "TEXT", nullable: false),
                    SourceWalletId = table.Column<string>(type: "TEXT", nullable: false),
                    DestinationWalletId = table.Column<string>(type: "TEXT", nullable: false),
                    TransactionType = table.Column<int>(type: "INTEGER", nullable: false),
                    TokenAmount = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.PaymentTransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Wallets_DestinationWalletId",
                        column: x => x.DestinationWalletId,
                        principalTable: "Wallets",
                        principalColumn: "WalledId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Wallets_SourceWalletId",
                        column: x => x.SourceWalletId,
                        principalTable: "Wallets",
                        principalColumn: "WalledId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DestinationWalletId",
                table: "Transactions",
                column: "DestinationWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SourceWalletId",
                table: "Transactions",
                column: "SourceWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_OwnerAuthorId",
                table: "Wallets",
                column: "OwnerAuthorId");

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

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Authors_EscrowWalletId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_MainWalletId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "EscrowWalletId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "MainWalletId",
                table: "Authors");

            migrationBuilder.AddColumn<long>(
                name: "EscrowTokens",
                table: "Authors",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ReadingTokens",
                table: "Authors",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
