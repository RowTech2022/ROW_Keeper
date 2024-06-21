using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Keeper.Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductDiscounts_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                schema: "new-keeper",
                table: "ProductDiscounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                schema: "new-keeper",
                table: "ProductDiscounts",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                schema: "new-keeper",
                table: "ProductDiscounts",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                schema: "new-keeper",
                table: "ProductDiscounts",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                schema: "new-keeper",
                table: "ProductDiscounts");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "new-keeper",
                table: "ProductDiscounts");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                schema: "new-keeper",
                table: "ProductDiscounts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "new-keeper",
                table: "ProductDiscounts");
        }
    }
}
