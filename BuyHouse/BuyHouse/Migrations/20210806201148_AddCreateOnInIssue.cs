using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuyHouse.Migrations
{
    public partial class AddCreateOnInIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateOn",
                table: "Issues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateOn",
                table: "Issues");
        }
    }
}
