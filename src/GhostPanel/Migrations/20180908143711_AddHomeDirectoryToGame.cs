using Microsoft.EntityFrameworkCore.Migrations;

namespace GameHostDemo.Migrations
{
    public partial class AddHomeDirectoryToGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HomeDirectory",
                table: "GameServers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomeDirectory",
                table: "GameServers");
        }
    }
}
