using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostPanel.Db.Migrations
{
    public partial class LinktoDefaultConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameDefaultConfigFileId",
                table: "GameServerConfigFile",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameServerConfigFile_GameDefaultConfigFileId",
                table: "GameServerConfigFile",
                column: "GameDefaultConfigFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameServerConfigFile_GameDefaultConfigFile_GameDefaultConfigFileId",
                table: "GameServerConfigFile",
                column: "GameDefaultConfigFileId",
                principalTable: "GameDefaultConfigFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameServerConfigFile_GameDefaultConfigFile_GameDefaultConfigFileId",
                table: "GameServerConfigFile");

            migrationBuilder.DropIndex(
                name: "IX_GameServerConfigFile_GameDefaultConfigFileId",
                table: "GameServerConfigFile");

            migrationBuilder.DropColumn(
                name: "GameDefaultConfigFileId",
                table: "GameServerConfigFile");
        }
    }
}
