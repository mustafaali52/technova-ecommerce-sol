using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace technova_ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class AlteringCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "DisplayOrder",
                table: "Category",
                newName: "display_order");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Category",
                newName: "category_name");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Category",
                newName: "category_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameColumn(
                name: "display_order",
                table: "Categories",
                newName: "DisplayOrder");

            migrationBuilder.RenameColumn(
                name: "category_name",
                table: "Categories",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CategoryId");
        }
    }
}
