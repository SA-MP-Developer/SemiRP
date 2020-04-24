using Microsoft.EntityFrameworkCore.Migrations;

namespace SemiRP.Migrations
{
    public partial class GunInHandItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "idWeapon",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemInHandId",
                table: "Characters",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_ItemInHandId",
                table: "Characters",
                column: "ItemInHandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Items_ItemInHandId",
                table: "Characters",
                column: "ItemInHandId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Items_ItemInHandId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_ItemInHandId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "idWeapon",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemInHandId",
                table: "Characters");

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "Items",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
