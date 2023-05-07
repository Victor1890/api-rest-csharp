using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_app_rest.Migrations
{
    /// <inheritdoc />
    public partial class ShopTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "shops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<int>(type: "int", nullable: true),
                    superMarketId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shops_products_productId",
                        column: x => x.productId,
                        principalTable: "products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_shops_supermarkets_superMarketId",
                        column: x => x.superMarketId,
                        principalTable: "supermarkets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_shops_productId",
                table: "shops",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_shops_superMarketId",
                table: "shops",
                column: "superMarketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shops");
        }
    }
}
