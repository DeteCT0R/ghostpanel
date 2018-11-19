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
                    Username = table.Column<string>(nullable: true)
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
                    FriendlyName = table.Column<string>(nullable: true),
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
                    GameProtocolId = table.Column<int>(nullable: false),
                    DefaultCommandline = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_GameProtocol_GameProtocolId",
                        column: x => x.GameProtocolId,
                        principalTable: "GameProtocol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameDefaultConfigFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FilePath = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Template = table.Column<string>(nullable: true),
                    GameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDefaultConfigFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameDefaultConfigFile_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<int>(nullable: true),
                    ModifiedById = table.Column<int>(nullable: true),
                    OwnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameServers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameServers_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameServers_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameServers_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameServers_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomVariable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VariableName = table.Column<string>(nullable: true),
                    VariableValue = table.Column<string>(nullable: true),
                    GameServerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomVariable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomVariable_GameServers_GameServerId",
                        column: x => x.GameServerId,
                        principalTable: "GameServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameServerConfigFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FilePath = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FileContent = table.Column<string>(nullable: true),
                    GameServerId = table.Column<int>(nullable: false),
                    GameDefaultConfigFileId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameServerConfigFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameServerConfigFile_GameDefaultConfigFile_GameDefaultConfigFileId",
                        column: x => x.GameDefaultConfigFileId,
                        principalTable: "GameDefaultConfigFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameServerConfigFile_GameServers_GameServerId",
                        column: x => x.GameServerId,
                        principalTable: "GameServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "ScheduledTask",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TaskName = table.Column<string>(nullable: true),
                    TaskType = table.Column<int>(nullable: false),
                    GameServerId = table.Column<int>(nullable: false),
                    Second = table.Column<string>(nullable: true, defaultValue: "*"),
                    Minute = table.Column<string>(nullable: true, defaultValue: "*"),
                    Hour = table.Column<string>(nullable: true, defaultValue: "*"),
                    DayOfMonth = table.Column<string>(nullable: true, defaultValue: "*"),
                    Month = table.Column<string>(nullable: true, defaultValue: "*"),
                    DayOfWeek = table.Column<string>(nullable: true, defaultValue: "*"),
                    LastRuntime = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<int>(nullable: true),
                    ModifiedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledTask_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduledTask_GameServers_GameServerId",
                        column: x => x.GameServerId,
                        principalTable: "GameServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduledTask_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomVariable_GameServerId",
                table: "CustomVariable",
                column: "GameServerId");

            migrationBuilder.CreateIndex(
                name: "IX_GameDefaultConfigFile_GameId",
                table: "GameDefaultConfigFile",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameProtocolId",
                table: "Games",
                column: "GameProtocolId");

            migrationBuilder.CreateIndex(
                name: "IX_GameServerConfigFile_GameDefaultConfigFileId",
                table: "GameServerConfigFile",
                column: "GameDefaultConfigFileId");

            migrationBuilder.CreateIndex(
                name: "IX_GameServerConfigFile_GameServerId",
                table: "GameServerConfigFile",
                column: "GameServerId");

            migrationBuilder.CreateIndex(
                name: "IX_GameServers_CreatedById",
                table: "GameServers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_GameServers_GameId",
                table: "GameServers",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameServers_ModifiedById",
                table: "GameServers",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_GameServers_OwnerId",
                table: "GameServers",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTask_CreatedById",
                table: "ScheduledTask",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTask_GameServerId",
                table: "ScheduledTask",
                column: "GameServerId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTask_ModifiedById",
                table: "ScheduledTask",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomVariable");

            migrationBuilder.DropTable(
                name: "GameServerConfigFile");

            migrationBuilder.DropTable(
                name: "GameServerCurrentStats");

            migrationBuilder.DropTable(
                name: "ScheduledTask");

            migrationBuilder.DropTable(
                name: "GameDefaultConfigFile");

            migrationBuilder.DropTable(
                name: "GameServers");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "GameProtocol");
        }
    }
}
