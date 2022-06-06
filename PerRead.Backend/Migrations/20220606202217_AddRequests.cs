using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerRead.Backend.Migrations
{
    public partial class AddRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    ArticleRequestId = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    TargetAuthorAuthorId = table.Column<string>(type: "TEXT", nullable: false),
                    InitiatorAuthorId = table.Column<string>(type: "TEXT", nullable: false),
                    RequestState = table.Column<int>(type: "INTEGER", nullable: false),
                    PostPublishState = table.Column<int>(type: "INTEGER", nullable: false),
                    PercentForledgers = table.Column<uint>(type: "INTEGER", nullable: false),
                    ResultingArticleArticleId = table.Column<string>(type: "TEXT", nullable: false),
                    Deadline = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.ArticleRequestId);
                    table.ForeignKey(
                        name: "FK_Requests_Articles_ResultingArticleArticleId",
                        column: x => x.ResultingArticleArticleId,
                        principalTable: "Articles",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Authors_InitiatorAuthorId",
                        column: x => x.InitiatorAuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Authors_TargetAuthorAuthorId",
                        column: x => x.TargetAuthorAuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pledges",
                columns: table => new
                {
                    RequestPledgeId = table.Column<string>(type: "TEXT", nullable: false),
                    ParentRequestArticleRequestId = table.Column<string>(type: "TEXT", nullable: true),
                    PledgerAuthorId = table.Column<string>(type: "TEXT", nullable: false),
                    TotalTokenSum = table.Column<uint>(type: "INTEGER", nullable: false),
                    TokensOnAccept = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pledges", x => x.RequestPledgeId);
                    table.ForeignKey(
                        name: "FK_Pledges_Authors_PledgerAuthorId",
                        column: x => x.PledgerAuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pledges_Requests_ParentRequestArticleRequestId",
                        column: x => x.ParentRequestArticleRequestId,
                        principalTable: "Requests",
                        principalColumn: "ArticleRequestId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pledges_ParentRequestArticleRequestId",
                table: "Pledges",
                column: "ParentRequestArticleRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Pledges_PledgerAuthorId",
                table: "Pledges",
                column: "PledgerAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_InitiatorAuthorId",
                table: "Requests",
                column: "InitiatorAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ResultingArticleArticleId",
                table: "Requests",
                column: "ResultingArticleArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_TargetAuthorAuthorId",
                table: "Requests",
                column: "TargetAuthorAuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pledges");

            migrationBuilder.DropTable(
                name: "Requests");
        }
    }
}
