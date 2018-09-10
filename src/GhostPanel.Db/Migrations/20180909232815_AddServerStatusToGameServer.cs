using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostPanel.Db.Migrations
{
    public partial class AddServerStatusToGameServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastPid",
                table: "GameServers",
                newName: "Pid");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "GameServers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "GameServers");

            migrationBuilder.RenameColumn(
                name: "Pid",
                table: "GameServers",
                newName: "LastPid");
        }
    }
}
