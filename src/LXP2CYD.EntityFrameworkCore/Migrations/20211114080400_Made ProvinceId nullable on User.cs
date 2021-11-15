using Microsoft.EntityFrameworkCore.Migrations;

namespace LXP2CYD.Migrations
{
    public partial class MadeProvinceIdnullableonUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_AppProvinces_ProvinceId",
                table: "AbpUsers");

            migrationBuilder.AlterColumn<int>(
                name: "ProvinceId",
                table: "AbpUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_AppProvinces_ProvinceId",
                table: "AbpUsers",
                column: "ProvinceId",
                principalTable: "AppProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_AppProvinces_ProvinceId",
                table: "AbpUsers");

            migrationBuilder.AlterColumn<int>(
                name: "ProvinceId",
                table: "AbpUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_AppProvinces_ProvinceId",
                table: "AbpUsers",
                column: "ProvinceId",
                principalTable: "AppProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
