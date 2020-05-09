using Microsoft.EntityFrameworkCore.Migrations;

namespace SemiRP.Migrations
{
    public partial class PhoneSilentAndAnonymMode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Anonym",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Silent",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anonym",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Silent",
                table: "Items");
        }
    }
}
