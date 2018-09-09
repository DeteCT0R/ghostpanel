using Microsoft.EntityFrameworkCore.Migrations;

namespace GameHostDemo.Migrations
{
    public partial class CommandlineToGameServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommandLine",
                table: "GameServers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommandLine",
                table: "GameServers");
        }
    }
}
