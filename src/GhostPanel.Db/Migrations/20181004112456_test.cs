using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostPanel.Db.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameServerConfigFile_GameServers_GameServerId",
                table: "GameServerConfigFile");

            migrationBuilder.DropIndex(
                name: "IX_GameDefaultConfigFile_GameId",
                table: "GameDefaultConfigFile");

            migrationBuilder.CreateIndex(
                name: "IX_GameDefaultConfigFile_GameId",
                table: "GameDefaultConfigFile",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameServerConfigFile_GameServers_GameServerId",
                table: "GameServerConfigFile",
                column: "GameServerId",
                principalTable: "GameServers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameServerConfigFile_GameServers_GameServerId",
                table: "GameServerConfigFile");

            migrationBuilder.DropIndex(
                name: "IX_GameDefaultConfigFile_GameId",
                table: "GameDefaultConfigFile");

            migrationBuilder.CreateIndex(
                name: "IX_GameDefaultConfigFile_GameId",
                table: "GameDefaultConfigFile",
                column: "GameId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GameServerConfigFile_GameServers_GameServerId",
                table: "GameServerConfigFile",
                column: "GameServerId",
                principalTable: "GameServers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
