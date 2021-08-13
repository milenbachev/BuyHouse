using Microsoft.EntityFrameworkCore.Migrations;

namespace BuyHouse.Migrations
{
    public partial class AddUserTableIsAgent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAgent",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAgent",
                table: "AspNetUsers");
        }
    }
}
