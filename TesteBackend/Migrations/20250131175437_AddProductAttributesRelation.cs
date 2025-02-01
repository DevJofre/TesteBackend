using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddProductAttributesRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Attribute",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Attribute",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProductId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Attribute",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProductId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_ProductId",
                table: "Attribute",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_Product_ProductId",
                table: "Attribute",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attribute_Product_ProductId",
                table: "Attribute");

            migrationBuilder.DropIndex(
                name: "IX_Attribute_ProductId",
                table: "Attribute");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Attribute");
        }
    }
}
