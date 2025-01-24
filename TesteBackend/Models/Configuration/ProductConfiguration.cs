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
        
        /* relations */
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(pt => pt.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        /* seed de valores */
        builder.HasData(
            new Product() { Id = 1, Name = "IPhone X", DateCreated = DateTime.Parse("2025-01-02"), CategoryId = 1},
            new Product() { Id = 2, Name = "IPhone IX", DateCreated = DateTime.Parse("2025-01-01"), CategoryId = 1},
            new Product() { Id = 3, Name = "IPhone XI", DateCreated = DateTime.Parse("2025-01-03"), CategoryId = 1}
        );
    }
}