using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerRead.Backend.Migrations
{
    public partial class OwnersCanBeEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CanBeRemoved",
                table: "ArticleOwners",
                newName: "CanBeEdited");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CanBeEdited",
                table: "ArticleOwners",
                newName: "CanBeRemoved");
        }
    }
}
