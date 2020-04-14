using Microsoft.EntityFrameworkCore.Migrations;

namespace SemiRP.Migrations
{
    public partial class ItemsHeritage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Containers_ContainerId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ContainerId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ContainerId",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "CurrentContainerId",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Items",
                nullable: false);

            migrationBuilder.AddColumn<bool>(
                name: "DefaultPhone",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Containers",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Containers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Items_CurrentContainerId",
                table: "Items",
                column: "CurrentContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_OwnerId",
                table: "Containers",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Characters_OwnerId",
                table: "Containers",
                column: "OwnerId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Containers_CurrentContainerId",
                table: "Items",
                column: "CurrentContainerId",
                principalTable: "Containers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Characters_OwnerId",
                table: "Containers");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Containers_CurrentContainerId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_CurrentContainerId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Containers_OwnerId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "CurrentContainerId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DefaultPhone",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "ContainerId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ContainerId",
                table: "Items",
                column: "ContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Containers_ContainerId",
                table: "Items",
                column: "ContainerId",
                principalTable: "Containers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
