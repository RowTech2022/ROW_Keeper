using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Keeper.Core.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDescriptionInCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "new-keeper",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                schema: "new-keeper",
                table: "Categories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                schema: "new-keeper",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "new-keeper",
                table: "Categories",
                type: "nvarchar(3000)",
                maxLength: 3000,
                nullable: true);
        }
    }
}
