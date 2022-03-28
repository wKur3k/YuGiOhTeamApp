using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YuGiOhTeamApp.Migrations
{
    public partial class addedIsLeaderFlaginUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isLeader",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isLeader",
                table: "Users");
        }
    }
}
