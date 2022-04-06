using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerRead.Backend.Migrations
{
    public partial class AddFeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feeds",
                columns: table => new
                {
                    FeedId = table.Column<string>(type: "TEXT", nullable: false),
                    FeedName = table.Column<string>(type: "TEXT", nullable: false),
                    OwnerAuthorId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feeds", x => x.FeedId);
                    table.ForeignKey(
                        name: "FK_Feeds_Authors_OwnerAuthorId",
                        column: x => x.OwnerAuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedAuthors",
                columns: table => new
                {
                    SubscribedAuthorsAuthorId = table.Column<string>(type: "TEXT", nullable: false),
                    SubscribedFeedsFeedId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedAuthors", x => new { x.SubscribedAuthorsAuthorId, x.SubscribedFeedsFeedId });
                    table.ForeignKey(
                        name: "FK_FeedAuthors_Authors_SubscribedAuthorsAuthorId",
                        column: x => x.SubscribedAuthorsAuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedAuthors_Feeds_SubscribedFeedsFeedId",
                        column: x => x.SubscribedFeedsFeedId,
                        principalTable: "Feeds",
                        principalColumn: "FeedId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedAuthors_SubscribedFeedsFeedId",
                table: "FeedAuthors",
                column: "SubscribedFeedsFeedId");

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_OwnerAuthorId",
                table: "Feeds",
                column: "OwnerAuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedAuthors");

            migrationBuilder.DropTable(
                name: "Feeds");
        }
    }
}
