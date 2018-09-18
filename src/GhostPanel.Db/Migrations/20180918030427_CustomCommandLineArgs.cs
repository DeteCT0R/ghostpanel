using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostPanel.Db.Migrations
{
    public partial class CustomCommandLineArgs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomCommandLineArgs",
                table: "GameServers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomCommandLineArgs",
                table: "GameServers");
        }
    }
}
