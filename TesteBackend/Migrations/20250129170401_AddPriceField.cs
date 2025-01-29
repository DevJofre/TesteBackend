using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 0f);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 0f);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Product");
        }
    }
}
