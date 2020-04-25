using Microsoft.EntityFrameworkCore.Migrations;

namespace SemiRP.Migrations
{
    public partial class ManyToManyVehicleBorrowers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Vehicles_VehicleDataId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_VehicleDataId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "VehicleDataId",
                table: "Characters");

            migrationBuilder.CreateTable(
                name: "VehicleDataBorrowers",
                columns: table => new
                {
                    VehicleId = table.Column<int>(nullable: false),
                    BorrowerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleDataBorrowers", x => new { x.BorrowerId, x.VehicleId });
                    table.ForeignKey(
                        name: "FK_VehicleDataBorrowers_Characters_BorrowerId",
                        column: x => x.BorrowerId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleDataBorrowers_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDataBorrowers_VehicleId",
                table: "VehicleDataBorrowers",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleDataBorrowers");

            migrationBuilder.AddColumn<int>(
                name: "VehicleDataId",
                table: "Characters",
                type: "int",
                nullable: true);

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
        }
    }
}
