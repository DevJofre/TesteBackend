using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TesteBackend.Migrations
{
    /// <inheritdoc />
    public partial class AttributesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attribute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Origem = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttribute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttributesId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttribute_Attribute_AttributesId",
                        column: x => x.AttributesId,
                        principalTable: "Attribute",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductAttribute_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Attribute",
                columns: new[] { "Id", "Brand", "Color", "DateCreated", "Origem" },
                values: new object[,]
                {
                    { 1, "Apple", "Preto", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Importado" },
                    { 2, "Samsung", "Branco", new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nacional" }
                });

            migrationBuilder.InsertData(
                table: "ProductAttribute",
                columns: new[] { "Id", "AttributesId", "DateCreated", "ProductId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 2, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_AttributesId",
                table: "ProductAttribute",
                column: "AttributesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_ProductId",
                table: "ProductAttribute",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAttribute");

            migrationBuilder.DropTable(
                name: "Attribute");
        }
    }
}
