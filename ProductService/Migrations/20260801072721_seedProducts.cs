using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductService.Migrations
{
    /// <inheritdoc />
    public partial class seedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Products",
                newName: "CreatedAt");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "Electronics", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gaming laptop", "Laptop", 1200m, 10 },
                    { 2, "Accessories", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wireless mouse", "Mouse", 20m, 50 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Products",
                newName: "createdAt");
        }
    }
}
