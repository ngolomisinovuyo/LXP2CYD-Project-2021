using Microsoft.EntityFrameworkCore.Migrations;

namespace LXP2CYD.Migrations
{
    public partial class AddedPhoneNumberandRegionIdonUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "AbpUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "AbpTenants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_RegionId",
                table: "AbpUsers",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_AppRegions_RegionId",
                table: "AbpUsers",
                column: "RegionId",
                principalTable: "AppRegions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_AppRegions_RegionId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_RegionId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "AbpTenants");
        }
    }
}
