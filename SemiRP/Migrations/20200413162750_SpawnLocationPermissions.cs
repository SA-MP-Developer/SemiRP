using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SemiRP.Migrations
{
    public partial class SpawnLocationPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name1",
                table: "Permissions");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Permissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Permissions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpawnLocationId",
                table: "Characters",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SpawnLocation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Interior = table.Column<int>(nullable: false),
                    VirtualWorld = table.Column<int>(nullable: false),
                    X = table.Column<float>(nullable: false),
                    Y = table.Column<float>(nullable: false),
                    Z = table.Column<float>(nullable: false),
                    RotX = table.Column<float>(nullable: false),
                    RotY = table.Column<float>(nullable: false),
                    RotZ = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpawnLocation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_AccountId",
                table: "Permissions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_SpawnLocationId",
                table: "Characters",
                column: "SpawnLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_SpawnLocation_SpawnLocationId",
                table: "Characters",
                column: "SpawnLocationId",
                principalTable: "SpawnLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Accounts_AccountId",
                table: "Permissions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_SpawnLocation_SpawnLocationId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Accounts_AccountId",
                table: "Permissions");

            migrationBuilder.DropTable(
                name: "SpawnLocation");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_AccountId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Characters_SpawnLocationId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "SpawnLocationId",
                table: "Characters");

            migrationBuilder.AddColumn<string>(
                name: "Name1",
                table: "Permissions",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
