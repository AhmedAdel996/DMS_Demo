using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DMS_Demo.Data.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shipping",
                columns: table => new
                {
                    Shipping_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Shipper_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Shipper_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Postal_Code = table.Column<int>(type: "int", nullable: true),
                    Phone = table.Column<long>(type: "bigint", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipping", x => x.Shipping_ID);
                });

            migrationBuilder.CreateTable(
                name: "Uoms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uoms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Order_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Shipping_ID = table.Column<int>(type: "int", nullable: true),
                    Order_Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Order_Status = table.Column<int>(type: "int", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Order_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Request_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Due_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Order_ID);
                    table.ForeignKey(
                        name: "FK_Order_Shipping_Shipping_ID",
                        column: x => x.Shipping_ID,
                        principalTable: "Shipping",
                        principalColumn: "Shipping_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Product_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uom_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Product_Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Product_Size = table.Column<int>(type: "int", nullable: false),
                    Product_Color = table.Column<int>(type: "int", nullable: false),
                    Adding_Date = table.Column<DateTime>(type: "date", nullable: false),
                    Stored_Quantity = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Product_ID);
                    table.ForeignKey(
                        name: "FK_Product_Uoms_Uom_Id",
                        column: x => x.Uom_Id,
                        principalTable: "Uoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetails_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_ID = table.Column<int>(type: "int", nullable: false),
                    Product_ID = table.Column<int>(type: "int", nullable: false),
                    UOM_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Quantity = table.Column<int>(type: "int", nullable: false),
                    Product_Color = table.Column<int>(type: "int", nullable: false),
                    Product_Size = table.Column<int>(type: "int", nullable: true),
                    Total_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetails_ID);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Order_Order_ID",
                        column: x => x.Order_ID,
                        principalTable: "Order",
                        principalColumn: "Order_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Product_Product_ID",
                        column: x => x.Product_ID,
                        principalTable: "Product",
                        principalColumn: "Product_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_Shipping_ID",
                table: "Order",
                column: "Shipping_ID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_Order_ID",
                table: "OrderDetails",
                column: "Order_ID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_Product_ID",
                table: "OrderDetails",
                column: "Product_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Uom_Id",
                table: "Product",
                column: "Uom_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Shipping");

            migrationBuilder.DropTable(
                name: "Uoms");
        }
    }
}
