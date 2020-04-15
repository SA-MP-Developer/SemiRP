using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SemiRP.Migrations
{
    public partial class PermissionRefacto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Accounts_AccountId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Characters_CharacterId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_AccountId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_CharacterId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Permissions");

            migrationBuilder.AddColumn<int>(
                name: "PermissionSet",
                table: "Characters",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PermissionSet",
                table: "Accounts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PermissionSets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionSetPermissions",
                columns: table => new
                {
                    PermissionSetId = table.Column<int>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionSetPermissions", x => new { x.PermissionId, x.PermissionSetId });
                    table.ForeignKey(
                        name: "FK_PermissionSetPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionSetPermissions_PermissionSets_PermissionSetId",
                        column: x => x.PermissionSetId,
                        principalTable: "PermissionSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_PermissionSet",
                table: "Characters",
                column: "PermissionSet");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PermissionSet",
                table: "Accounts",
                column: "PermissionSet");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionSetPermissions_PermissionSetId",
                table: "PermissionSetPermissions",
                column: "PermissionSetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_PermissionSets_PermissionSet",
                table: "Accounts",
                column: "PermissionSet",
                principalTable: "PermissionSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_PermissionSets_PermissionSet",
                table: "Characters",
                column: "PermissionSet",
                principalTable: "PermissionSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_PermissionSets_PermissionSet",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_PermissionSets_PermissionSet",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "PermissionSetPermissions");

            migrationBuilder.DropTable(
                name: "PermissionSets");

            migrationBuilder.DropIndex(
                name: "IX_Characters_PermissionSet",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_PermissionSet",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PermissionSet",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "PermissionSet",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Permissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "Permissions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_AccountId",
                table: "Permissions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_CharacterId",
                table: "Permissions",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Accounts_AccountId",
                table: "Permissions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Characters_CharacterId",
                table: "Permissions",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
