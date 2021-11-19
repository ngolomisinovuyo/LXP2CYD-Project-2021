using Microsoft.EntityFrameworkCore.Migrations;

namespace LXP2CYD.Migrations
{
    public partial class Addedtypeonstaff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppStaffs_UserId",
                table: "AppStaffs");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AppStaffs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AppStaffs_UserId",
                table: "AppStaffs",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppLearners_UserId",
                table: "AppLearners",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppLearners_AbpUsers_UserId",
                table: "AppLearners",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppLearners_AbpUsers_UserId",
                table: "AppLearners");

            migrationBuilder.DropIndex(
                name: "IX_AppStaffs_UserId",
                table: "AppStaffs");

            migrationBuilder.DropIndex(
                name: "IX_AppLearners_UserId",
                table: "AppLearners");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AppStaffs");

            migrationBuilder.CreateIndex(
                name: "IX_AppStaffs_UserId",
                table: "AppStaffs",
                column: "UserId");
        }
    }
}
