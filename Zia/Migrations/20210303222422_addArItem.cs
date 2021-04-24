using Microsoft.EntityFrameworkCore.Migrations;

namespace Zia.Migrations
{
    public partial class addArItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArLongDis",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArName",
                table: "Items",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArShortName",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArSpecifications",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArLongDis",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ArName",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ArShortName",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ArSpecifications",
                table: "Items");
        }
    }
}
