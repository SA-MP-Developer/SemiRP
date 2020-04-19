using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SemiRP.Migrations
{
    public partial class PhoneBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxContact",
                table: "Items",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContactPhone",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    PhoneId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPhone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactPhone_Items_PhoneId",
                        column: x => x.PhoneId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactPhone_PhoneId",
                table: "ContactPhone",
                column: "PhoneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactPhone");

            migrationBuilder.DropColumn(
                name: "MaxContact",
                table: "Items");
        }
    }
}
