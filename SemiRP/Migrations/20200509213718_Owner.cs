using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SemiRP.Migrations
{
    public partial class Owner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Characters_OwnerId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "VehicleDataBorrowers");

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CharacterId = table.Column<int>(nullable: true),
                    GroupId = table.Column<int>(nullable: true),
                    VehicleDataId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Owners_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Owners_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Owners_Vehicles_VehicleDataId",
                        column: x => x.VehicleDataId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Owners_CharacterId",
                table: "Owners",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_GroupId",
                table: "Owners",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_VehicleDataId",
                table: "Owners",
                column: "VehicleDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Owners_OwnerId",
                table: "Vehicles",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Owners_OwnerId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.CreateTable(
                name: "VehicleDataBorrowers",
                columns: table => new
                {
                    BorrowerId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Characters_OwnerId",
                table: "Vehicles",
                column: "OwnerId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
