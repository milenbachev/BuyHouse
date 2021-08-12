using Microsoft.EntityFrameworkCore.Migrations;

namespace BuyHouse.Migrations
{
    public partial class FixNameOfCategoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Categories_CategotyId",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "CategotyId",
                table: "Properties",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_CategotyId",
                table: "Properties",
                newName: "IX_Properties_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Categories_CategoryId",
                table: "Properties",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Categories_CategoryId",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Properties",
                newName: "CategotyId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_CategoryId",
                table: "Properties",
                newName: "IX_Properties_CategotyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Categories_CategotyId",
                table: "Properties",
                column: "CategotyId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
