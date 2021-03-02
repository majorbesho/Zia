using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zia.Data.Migrations
{
    public partial class OrerheaderAndDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    OrderTotalOrginal = table.Column<double>(nullable: false),
                    orderTotal = table.Column<double>(nullable: false),
                    PickUptime = table.Column<DateTime>(nullable: false),
                    CoupinCode = table.Column<string>(nullable: true),
                    coupinDiscount = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    PaymentStatus = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    TelePhone = table.Column<string>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true),
                    PickupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHeaders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetailses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    count = table.Column<int>(nullable: false),
                    Discription = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetailses_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetailses_OrderHeaders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailses_ItemId",
                table: "OrderDetailses",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailses_OrderId",
                table: "OrderDetailses",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_UserId",
                table: "OrderHeaders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetailses");

            migrationBuilder.DropTable(
                name: "OrderHeaders");
        }
    }
}
