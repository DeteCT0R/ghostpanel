using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostPanel.Db.Migrations
{
    public partial class ChangeGameServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbsolutePath",
                table: "GameServers");

            migrationBuilder.RenameColumn(
                name: "StartPath",
                table: "GameServers",
                newName: "StartDirectory");

            migrationBuilder.RenameColumn(
                name: "RelativePath",
                table: "GameServers",
                newName: "HomeDirectory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDirectory",
                table: "GameServers",
                newName: "StartPath");

            migrationBuilder.RenameColumn(
                name: "HomeDirectory",
                table: "GameServers",
                newName: "RelativePath");

            migrationBuilder.AddColumn<string>(
                name: "AbsolutePath",
                table: "GameServers",
                nullable: true);
        }
    }
}
