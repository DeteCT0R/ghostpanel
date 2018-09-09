using Microsoft.EntityFrameworkCore.Migrations;

namespace GameHostDemo.Migrations
{
    public partial class NullPid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LastPid",
                table: "GameServers",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LastPid",
                table: "GameServers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
