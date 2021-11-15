using Microsoft.EntityFrameworkCore.Migrations;

namespace LXP2CYD.Migrations
{
    public partial class MaderegionIdnullableonTenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpTenants_AppRegions_RegionId",
                table: "AbpTenants");

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "AbpTenants",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpTenants_AppRegions_RegionId",
                table: "AbpTenants",
                column: "RegionId",
                principalTable: "AppRegions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpTenants_AppRegions_RegionId",
                table: "AbpTenants");

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "AbpTenants",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpTenants_AppRegions_RegionId",
                table: "AbpTenants",
                column: "RegionId",
                principalTable: "AppRegions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
