using Microsoft.EntityFrameworkCore.Migrations;

namespace GhostPanel.Db.Migrations
{
    public partial class ChangeConfigColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Template",
                table: "GameServerConfigFile",
                newName: "FileContent");

            migrationBuilder.AddColumn<string>(
                name: "VariableName",
                table: "CustomVariable",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VariableValue",
                table: "CustomVariable",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VariableName",
                table: "CustomVariable");

            migrationBuilder.DropColumn(
                name: "VariableValue",
                table: "CustomVariable");

            migrationBuilder.RenameColumn(
                name: "FileContent",
                table: "GameServerConfigFile",
                newName: "Template");
        }
    }
}
