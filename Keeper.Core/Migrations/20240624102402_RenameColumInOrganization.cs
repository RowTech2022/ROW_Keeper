using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Keeper.Core.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumInOrganization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OwnerEmiail",
                schema: "new-keeper",
                table: "Organizations",
                newName: "OwnerEmail");

            migrationBuilder.RenameColumn(
                name: "OrgStatus",
                schema: "new-keeper",
                table: "Organizations",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "new-keeper",
                table: "Organizations",
                newName: "OrgStatus");

            migrationBuilder.RenameColumn(
                name: "OwnerEmail",
                schema: "new-keeper",
                table: "Organizations",
                newName: "OwnerEmiail");
        }
    }
}
