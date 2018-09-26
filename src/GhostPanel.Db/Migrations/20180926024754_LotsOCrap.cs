using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostPanel.Db.Migrations
{
    public partial class LotsOCrap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentPlayerCount",
                table: "GameServerCurrentStat",
                newName: "CurrentPlayers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "GameServerCurrentStat",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentPlayers",
                table: "GameServerCurrentStat",
                newName: "CurrentPlayerCount");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "GameServerCurrentStat",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
