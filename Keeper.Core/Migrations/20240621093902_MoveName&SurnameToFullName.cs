using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Keeper.Core.Migrations
{
    /// <inheritdoc />
    public partial class MoveNameSurnameToFullName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "new-keeper",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Surname",
                schema: "new-keeper",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "new-keeper",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrgDescription",
                schema: "new-keeper",
                table: "Organizations",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                schema: "new-keeper",
                table: "OrganizationBranches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "new-keeper",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                schema: "new-keeper",
                table: "Users",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Phone",
                schema: "new-keeper",
                table: "Users",
                column: "Phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                schema: "new-keeper",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Login",
                schema: "new-keeper",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Phone",
                schema: "new-keeper",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "new-keeper",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OrgDescription",
                schema: "new-keeper",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "new-keeper",
                table: "OrganizationBranches");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "new-keeper",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                schema: "new-keeper",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
