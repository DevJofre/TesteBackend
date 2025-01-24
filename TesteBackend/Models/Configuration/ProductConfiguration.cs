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
    }
}