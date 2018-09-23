using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostPanel.Db.Migrations
{
    public partial class BunchOfChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pid",
                table: "GameServers");

            migrationBuilder.DropColumn(
                name: "RestartAttempts",
                table: "GameServers");

            migrationBuilder.CreateTable(
                name: "GameServerCurrentStat",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Pid = table.Column<int>(nullable: true),
                    Map = table.Column<string>(nullable: true),
                    CurrentPlayers = table.Column<int>(nullable: false),
                    MaxPlayers = table.Column<int>(nullable: false),
                    Name = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    RestartAttempts = table.Column<int>(nullable: false),
                    ServerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameServerCurrentStat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameServerCurrentStat_GameServers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "GameServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameServerCurrentStat_ServerId",
                table: "GameServerCurrentStat",
                column: "ServerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameServerCurrentStat");

            migrationBuilder.AddColumn<int>(
                name: "Pid",
                table: "GameServers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RestartAttempts",
                table: "GameServers",
                nullable: false,
                defaultValue: 0);
        }
    }
}
