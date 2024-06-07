using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Keeper.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddProductSupplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateAt",
                schema: "new-keeper",
                table: "UserRole",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "new-keeper",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OrgEmail",
                schema: "new-keeper",
                table: "Organizations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchEmail",
                schema: "new-keeper",
                table: "OrganizationBranches",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "new-keeper",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OrgEmail",
                schema: "new-keeper",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "BranchEmail",
                schema: "new-keeper",
                table: "OrganizationBranches");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "new-keeper",
                table: "UserRole",
                newName: "CreateAt");
        }
    }
}
