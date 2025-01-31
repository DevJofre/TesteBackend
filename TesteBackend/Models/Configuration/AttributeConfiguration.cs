using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteBackend.Models;

namespace TesteBackend.Models.Configuration;

internal sealed class AttributeConfiguration : IEntityTypeConfiguration<Attribute>
{
    public void Configure(EntityTypeBuilder<Attribute> builder)
    {
        builder.ToTable(nameof(Attribute));
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd();
        builder.Property(a => a.DateCreated).IsRequired();
        builder.Property(a => a.Brand).HasMaxLength(25).IsRequired();
        builder.Property(a => a.Color).HasMaxLength(25).IsRequired();
        builder.Property(a => a.Origem).HasConversion<string>().HasMaxLength(20);

        builder.HasData(
            new Attribute { Id = 1, DateCreated = DateTime.Parse("2025-01-01"), Brand = "Apple", Color = "Preto", Origem = OrigemProduto.Importado },
            new Attribute { Id = 2, DateCreated = DateTime.Parse("2025-01-02"), Brand = "Samsung", Color = "Branco", Origem = OrigemProduto.Nacional }
        );
    }
}