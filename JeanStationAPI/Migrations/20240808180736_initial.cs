using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeanStationAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    custId = table.Column<int>(type: "int", nullable: false),
                    itemCode = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    price = table.Column<double>(type: "float", nullable: true),
                    qty = table.Column<int>(type: "int", nullable: true),
                    value = table.Column<double>(type: "float", nullable: true, computedColumnSql: "([qty]*[price])", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cart__9D07223B6506D53D", x => new { x.custId, x.itemCode });
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    custId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    custName = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    phoneNo = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    userName = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    pwd = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__9725F2C6A1FC69A3", x => x.custId);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    empId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    empName = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    empEmail = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    empPhoneNo = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    empUserName = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    empPwd = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    storeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__AFB3EC0D384C8BBE", x => x.empId);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    itemCode = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    itemName = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    storeId = table.Column<int>(type: "int", nullable: true),
                    item_image = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Item__A22D0FD19AB3A1B9", x => x.itemCode);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    orderId = table.Column<int>(type: "int", nullable: false),
                    itemCode = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    custId = table.Column<int>(type: "int", nullable: true),
                    orderStatus = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true, defaultValue: "order placed"),
                    orderDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    amount = table.Column<double>(type: "float", nullable: true),
                    qty = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<double>(type: "float", nullable: true),
                    itemValue = table.Column<double>(type: "float", nullable: true, computedColumnSql: "([qty]*[price])", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__orders__022BE3A015B3B57C", x => new { x.orderId, x.itemCode });
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    storeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    storeName = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    location = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    phoneNo = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Store__1EA71613B00203F8", x => x.storeId);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Customer__66DCF95C595F3A2D",
                table: "Customer",
                column: "userName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Employee__C7790080232A834E",
                table: "Employee",
                column: "empUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "Store");
        }
    }
}
