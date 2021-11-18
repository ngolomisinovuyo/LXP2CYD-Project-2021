using Microsoft.EntityFrameworkCore.Migrations;

namespace LXP2CYD.Migrations
{
    public partial class ChangeddatatypeofEventCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventCategory",
                table: "AppProgrammes");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "AppProgrammes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "AppProgrammes");

            migrationBuilder.AddColumn<string>(
                name: "EventCategory",
                table: "AppProgrammes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
