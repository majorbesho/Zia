using Microsoft.EntityFrameworkCore.Migrations;

namespace Zia.Data.Migrations
{
    public partial class RecreateShoopingcart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopingCarts_Items_ItemId",
                table: "ShopingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShopingCarts_ItemId",
                table: "ShopingCarts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ShopingCarts_ItemId",
                table: "ShopingCarts",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopingCarts_Items_ItemId",
                table: "ShopingCarts",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
