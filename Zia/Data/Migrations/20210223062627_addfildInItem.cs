using Microsoft.EntityFrameworkCore.Migrations;

namespace Zia.Data.Migrations
{
    public partial class addfildInItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dis",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "LongDis",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specifications",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "shortDis",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongDis",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Specifications",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "shortDis",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "Dis",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
