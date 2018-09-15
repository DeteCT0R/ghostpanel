using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostPanel.Db.Migrations
{
    public partial class AddArchiveNameToGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "GameServers");

            migrationBuilder.AddColumn<string>(
                name: "ArchiveName",
                table: "Games",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArchiveName",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "GameServers",
                nullable: false,
                defaultValue: 0);
        }
    }
}
