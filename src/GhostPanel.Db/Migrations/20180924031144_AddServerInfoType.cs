using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostPanel.Db.Migrations
{
    public partial class AddServerInfoType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServerInfoType",
                table: "GameProtocol",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServerInfoType",
                table: "GameProtocol");
        }
    }
}
