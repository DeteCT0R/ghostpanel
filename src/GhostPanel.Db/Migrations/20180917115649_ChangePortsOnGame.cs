using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostPanel.Db.Migrations
{
    public partial class ChangePortsOnGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Port",
                table: "GameServers",
                newName: "QueryPort");

            migrationBuilder.AddColumn<int>(
                name: "GamePort",
                table: "GameServers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GamePort",
                table: "GameServers");

            migrationBuilder.RenameColumn(
                name: "QueryPort",
                table: "GameServers",
                newName: "Port");
        }
    }
}
