using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_app_rest.Migrations
{
    /// <inheritdoc />
    public partial class AddStockProductBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "products-brands",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "products-brands");
        }
    }
}
