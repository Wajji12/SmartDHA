using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DHAFacilitationAPIs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserFamiliestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFamily_ApplicationUser_ApplicationUserId",
                table: "UserFamily");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFamily",
                table: "UserFamily");

            migrationBuilder.RenameTable(
                name: "UserFamily",
                newName: "UserFamilies");

            migrationBuilder.RenameIndex(
                name: "IX_UserFamily_ApplicationUserId",
                table: "UserFamilies",
                newName: "IX_UserFamilies_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFamilies",
                table: "UserFamilies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFamilies_ApplicationUser_ApplicationUserId",
                table: "UserFamilies",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFamilies_ApplicationUser_ApplicationUserId",
                table: "UserFamilies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFamilies",
                table: "UserFamilies");

            migrationBuilder.RenameTable(
                name: "UserFamilies",
                newName: "UserFamily");

            migrationBuilder.RenameIndex(
                name: "IX_UserFamilies_ApplicationUserId",
                table: "UserFamily",
                newName: "IX_UserFamily_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFamily",
                table: "UserFamily",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFamily_ApplicationUser_ApplicationUserId",
                table: "UserFamily",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
