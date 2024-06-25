using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Keeper.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddedPlanModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationBranches",
                schema: "new-keeper");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                schema: "new-keeper",
                table: "Users",
                newName: "OrgId");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                schema: "new-keeper",
                table: "Products",
                newName: "OrgId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_UPC_BranchId",
                schema: "new-keeper",
                table: "Products",
                newName: "IX_Products_UPC_OrgId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BranchId",
                schema: "new-keeper",
                table: "Products",
                newName: "IX_Products_OrgId");

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                schema: "new-keeper",
                table: "ProductDiscounts",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                schema: "new-keeper",
                table: "ProductDiscounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrgStatus",
                schema: "new-keeper",
                table: "Organizations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OwnerEmiail",
                schema: "new-keeper",
                table: "Organizations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerFullName",
                schema: "new-keeper",
                table: "Organizations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerPhone",
                schema: "new-keeper",
                table: "Organizations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Plans",
                schema: "new-keeper",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReqUserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductPurchaseDetails",
                schema: "new-keeper",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReqUserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpireDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPurchaseDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductPurchases",
                schema: "new-keeper",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReqUserId = table.Column<int>(type: "int", nullable: false),
                    OrgId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    BankAccount = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPurchases", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plans",
                schema: "new-keeper");

            migrationBuilder.DropTable(
                name: "ProductPurchaseDetails",
                schema: "new-keeper");

            migrationBuilder.DropTable(
                name: "ProductPurchases",
                schema: "new-keeper");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                schema: "new-keeper",
                table: "ProductDiscounts");

            migrationBuilder.DropColumn(
                name: "OrgStatus",
                schema: "new-keeper",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "OwnerEmiail",
                schema: "new-keeper",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "OwnerFullName",
                schema: "new-keeper",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "OwnerPhone",
                schema: "new-keeper",
                table: "Organizations");

            migrationBuilder.RenameColumn(
                name: "OrgId",
                schema: "new-keeper",
                table: "Users",
                newName: "BranchId");

            migrationBuilder.RenameColumn(
                name: "OrgId",
                schema: "new-keeper",
                table: "Products",
                newName: "BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_UPC_OrgId",
                schema: "new-keeper",
                table: "Products",
                newName: "IX_Products_UPC_BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_OrgId",
                schema: "new-keeper",
                table: "Products",
                newName: "IX_Products_BranchId");

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                schema: "new-keeper",
                table: "ProductDiscounts",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.CreateTable(
                name: "OrganizationBranches",
                schema: "new-keeper",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    BranchAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BranchEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BranchPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    ReqUserId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationBranches", x => x.Id);
                });
        }
    }
}
