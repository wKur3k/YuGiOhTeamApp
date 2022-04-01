using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YuGiOhTeamApp.Migrations
{
    public partial class AddedAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Decklists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Decklists");
        }
    }
}
