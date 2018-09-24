using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostPanel.Db.Migrations
{
    public partial class BunchOfChanges2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentPlayers",
                table: "GameServerCurrentStat",
                newName: "CurrentPlayerCount");

            migrationBuilder.AddColumn<int>(
                name: "ProtocolId",
                table: "GameServers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GameProtocol",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FullTypeName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameProtocol", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameServers_ProtocolId",
                table: "GameServers",
                column: "ProtocolId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameServers_GameProtocol_ProtocolId",
                table: "GameServers",
                column: "ProtocolId",
                principalTable: "GameProtocol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameServers_GameProtocol_ProtocolId",
                table: "GameServers");

            migrationBuilder.DropTable(
                name: "GameProtocol");

            migrationBuilder.DropIndex(
                name: "IX_GameServers_ProtocolId",
                table: "GameServers");

            migrationBuilder.DropColumn(
                name: "ProtocolId",
                table: "GameServers");

            migrationBuilder.RenameColumn(
                name: "CurrentPlayerCount",
                table: "GameServerCurrentStat",
                newName: "CurrentPlayers");
        }
    }
}
