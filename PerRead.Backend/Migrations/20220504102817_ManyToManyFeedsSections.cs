using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerRead.Backend.Migrations
{
    public partial class ManyToManyFeedsSections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Feeds_FeedId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_FeedId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "FeedId",
                table: "Sections");

            migrationBuilder.CreateTable(
                name: "SectionFeedMapping",
                columns: table => new
                {
                    SectionId = table.Column<string>(type: "TEXT", nullable: false),
                    FeedId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionFeedMapping", x => new { x.SectionId, x.FeedId });
                    table.ForeignKey(
                        name: "FK_SectionFeedMapping_Feeds_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feeds",
                        principalColumn: "FeedId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SectionFeedMapping_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "SectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SectionFeedMapping_FeedId",
                table: "SectionFeedMapping",
                column: "FeedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SectionFeedMapping");

            migrationBuilder.AddColumn<string>(
                name: "FeedId",
                table: "Sections",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sections_FeedId",
                table: "Sections",
                column: "FeedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Feeds_FeedId",
                table: "Sections",
                column: "FeedId",
                principalTable: "Feeds",
                principalColumn: "FeedId");
        }
    }
}
