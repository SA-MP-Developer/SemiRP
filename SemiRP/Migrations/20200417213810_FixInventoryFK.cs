using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SemiRP.Migrations
{
    public partial class FixInventoryFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Containers_InventoryId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Containers_ContainerTypes_TypeId",
                table: "Containers");

            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Characters_OwnerId",
                table: "Containers");

            migrationBuilder.DropTable(
                name: "ContainerTypes");

            migrationBuilder.DropIndex(
                name: "IX_Containers_TypeId",
                table: "Containers");

            migrationBuilder.DropIndex(
                name: "IX_Containers_OwnerId",
                table: "Containers");

            migrationBuilder.DropIndex(
                name: "IX_Characters_InventoryId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "MaxSpace",
                table: "Containers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Character",
                table: "Containers",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "Skin",
                table: "Characters",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.CreateIndex(
                name: "IX_Containers_Character",
                table: "Containers",
                column: "Character",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Characters_Character",
                table: "Containers",
                column: "Character",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Characters_Character",
                table: "Containers");

            migrationBuilder.DropIndex(
                name: "IX_Containers_Character",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "MaxSpace",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "Character",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "Skin",
                table: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Containers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Containers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "Characters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContainerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MaxSpace = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Containers_TypeId",
                table: "Containers",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_OwnerId",
                table: "Containers",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_InventoryId",
                table: "Characters",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Containers_InventoryId",
                table: "Characters",
                column: "InventoryId",
                principalTable: "Containers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_ContainerTypes_TypeId",
                table: "Containers",
                column: "TypeId",
                principalTable: "ContainerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Characters_OwnerId",
                table: "Containers",
                column: "OwnerId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
