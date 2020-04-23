using Microsoft.EntityFrameworkCore.Migrations;

namespace SemiRP.Migrations
{
    public partial class AddVehicles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdVehicle",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "Color1",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Color2",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Dammages",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Fuel",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Model",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpawnLocationId",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehicleDataId",
                table: "Characters",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_SpawnLocationId",
                table: "Vehicles",
                column: "SpawnLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_VehicleDataId",
                table: "Characters",
                column: "VehicleDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Vehicles_VehicleDataId",
                table: "Characters",
                column: "VehicleDataId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_SpawnLocation_SpawnLocationId",
                table: "Vehicles",
                column: "SpawnLocationId",
                principalTable: "SpawnLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Vehicles_VehicleDataId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_SpawnLocation_SpawnLocationId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_SpawnLocationId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Characters_VehicleDataId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Color1",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Color2",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Dammages",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Fuel",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "SpawnLocationId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "VehicleDataId",
                table: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "IdVehicle",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
