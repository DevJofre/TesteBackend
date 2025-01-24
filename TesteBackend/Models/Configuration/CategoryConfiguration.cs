using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TesteBackend.Models.Configuration;

internal sealed class CategoryConfiguration: IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(nameof(Category));
        builder.HasKey(x => x.Id);
        builder.Property(pt => pt.Id).ValueGeneratedOnAdd();
        builder.Property(pt => pt.Name).HasMaxLength(100);
        
        /* relations */
        builder.HasMany(c => c.Products).WithOne(p => p.Category);

        /* seed de valores */
        builder.HasData(
            new Category() { Id = 1, Name = "Phones", DateCreated = DateTime.Parse("2025-01-01")}
        );
    }
}