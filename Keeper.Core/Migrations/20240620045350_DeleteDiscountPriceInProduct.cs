using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Keeper.Core.Migrations
{
    /// <inheritdoc />
    public partial class DeleteDiscountPriceInProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_UPC",
                schema: "new-keeper",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                schema: "new-keeper",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "HaveDiscount",
                schema: "new-keeper",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ProductDitscounts",
                schema: "new-keeper",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReqUserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Percent = table.Column<double>(type: "float", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    FromDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ToDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDitscounts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_UPC_BranchId",
                schema: "new-keeper",
                table: "Products",
                columns: new[] { "UPC", "BranchId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDitscounts",
                schema: "new-keeper");

            migrationBuilder.DropIndex(
                name: "IX_Products_UPC_BranchId",
                schema: "new-keeper",
                table: "Products");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPrice",
                schema: "new-keeper",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "HaveDiscount",
                schema: "new-keeper",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UPC",
                schema: "new-keeper",
                table: "Products",
                column: "UPC",
                unique: true);
        }
    }
}
