using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TesteBackend.Models.Configuration;

internal sealed class ProductConfiguration: IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product));
        builder.HasKey(x => x.Id);
        builder.Property(pt => pt.Id).ValueGeneratedOnAdd();
        builder.Property(pt => pt.Name).HasMaxLength(100);

        /* seed de valores */
        builder.HasData(
            new Product() { Id = 1, Name = "IPhone X", Date = DateTime.Parse("2025-01-02")},
            new Product() { Id = 2, Name = "IPhone IX", Date = DateTime.Parse("2025-01-01")},
            new Product() { Id = 3, Name = "IPhone XI", Date = DateTime.Parse("2025-01-03")}
        );
    }
}