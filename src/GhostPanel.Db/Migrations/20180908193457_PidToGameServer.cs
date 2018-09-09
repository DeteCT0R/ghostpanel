using Microsoft.EntityFrameworkCore.Migrations;

namespace GameHostDemo.Migrations
{
    public partial class PidToGameServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastPid",
                table: "GameServers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPid",
                table: "GameServers");
        }
    }
}
