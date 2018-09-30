using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostPanel.Db.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameProtocol",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FullTypeName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ServerInfoType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameProtocol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    GameServerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    SteamAppId = table.Column<int>(nullable: true),
                    ArchiveName = table.Column<string>(nullable: true),
                    SteamUrl = table.Column<string>(nullable: true),
                    ExeName = table.Column<string>(nullable: true),
                    MaxSlots = table.Column<int>(nullable: false),
                    MinSlots = table.Column<int>(nullable: false),
                    DefaultSlots = table.Column<int>(nullable: false),
                    DefaultPath = table.Column<string>(nullable: true),
                    GamePort = table.Column<int>(nullable: false),
                    QueryPort = table.Column<int>(nullable: false),
                    PortIncrement = table.Column<int>(nullable: false),
                    GameProtocolId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_GameProtocol_GameProtocolId",
                        column: x => x.GameProtocolId,
                        principalTable: "GameProtocol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameServers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GameId = table.Column<int>(nullable: false),
                    IpAddress = table.Column<string>(nullable: true),
                    GamePort = table.Column<int>(nullable: false),
                    QueryPort = table.Column<int>(nullable: false),
                    ServerName = table.Column<string>(nullable: true),
                    IsEnabled = table.Column<bool>(nullable: false),
                    Version = table.Column<string>(nullable: true),
                    StartDirectory = table.Column<string>(nullable: true),
                    HomeDirectory = table.Column<string>(nullable: true),
                    CommandLine = table.Column<string>(nullable: true),
                    Slots = table.Column<int>(nullable: false),
                    RconPassword = table.Column<string>(nullable: true),
                    Guid = table.Column<Guid>(nullable: false),
                    CustomCommandLineArgs = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameServers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameServers_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameServers_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameServerCurrentStats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Pid = table.Column<int>(nullable: true),
                    Map = table.Column<string>(nullable: true),
                    CurrentPlayers = table.Column<int>(nullable: false),
                    MaxPlayers = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    RestartAttempts = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameServerCurrentStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameServerCurrentStats_GameServers_Id",
                        column: x => x.Id,
                        principalTable: "GameServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameProtocolId",
                table: "Games",
                column: "GameProtocolId");

            migrationBuilder.CreateIndex(
                name: "IX_GameServers_GameId",
                table: "GameServers",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameServers_UserId",
                table: "GameServers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameServerCurrentStats");

            migrationBuilder.DropTable(
                name: "GameServers");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "GameProtocol");
        }
    }
}
