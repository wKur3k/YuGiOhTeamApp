using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YuGiOhTeamApp.Migrations
{
    public partial class removedAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Decklists");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Decklists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
