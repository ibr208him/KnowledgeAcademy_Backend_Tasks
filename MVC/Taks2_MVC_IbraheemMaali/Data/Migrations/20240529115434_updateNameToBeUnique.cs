using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Project_BookStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateNameToBeUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_categories_Name",
                table: "categories",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_categories_Name",
                table: "categories");
        }
    }
}
