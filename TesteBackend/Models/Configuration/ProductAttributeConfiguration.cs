using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteBackend.Models;

namespace TesteBackend.Models.Configuration;

internal sealed class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
{
    public void Configure(EntityTypeBuilder<ProductAttribute> builder)
    {
        builder.ToTable(nameof(ProductAttribute));
        builder.HasKey(pa => pa.Id);
        builder.Property(pa => pa.Id).ValueGeneratedOnAdd();
        builder.Property(pa => pa.DateCreated).IsRequired();

        builder.HasOne(pa => pa.Attribute)
            .WithMany()
            .HasForeignKey(pa => pa.AttributesId);

        builder.HasOne(pa => pa.Product)
            .WithMany()
            .HasForeignKey(pa => pa.ProductId);

        builder.HasData(
            new ProductAttribute { Id = 1, DateCreated = DateTime.Parse("2025-01-01"), AttributesId = 1, ProductId = 1 },
            new ProductAttribute { Id = 2, DateCreated = DateTime.Parse("2025-01-02"), AttributesId = 2, ProductId = 2 }
        );
    }
}