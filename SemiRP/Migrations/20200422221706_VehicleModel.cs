using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SemiRP.Migrations
{
    public partial class VehicleModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Model",
                table: "Vehicles");

            migrationBuilder.AddColumn<float>(
                name: "FuelConsumption",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MaxFuel",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Mileage",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpawnLocationId",
                table: "Items",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VehicleModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Model = table.Column<int>(nullable: false),
                    BasePrice = table.Column<int>(nullable: false),
                    MaxFuel = table.Column<float>(nullable: false),
                    FuelConsumption = table.Column<float>(nullable: false),
                    ContainerSize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModels", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_TypeId",
                table: "Vehicles",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SpawnLocationId",
                table: "Items",
                column: "SpawnLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_SpawnLocation_SpawnLocationId",
                table: "Items",
                column: "SpawnLocationId",
                principalTable: "SpawnLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleModels_TypeId",
                table: "Vehicles",
                column: "TypeId",
                principalTable: "VehicleModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_SpawnLocation_SpawnLocationId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleModels_TypeId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "VehicleModels");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_TypeId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Items_SpawnLocationId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "FuelConsumption",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "MaxFuel",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Mileage",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "SpawnLocationId",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "Model",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
