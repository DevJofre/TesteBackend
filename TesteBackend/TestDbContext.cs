using Microsoft.EntityFrameworkCore;
using TesteBackend.Models;
using AttributeModel = TesteBackend.Models.Attribute;

namespace TesteBackend;

public class TestDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<AttributeModel> Attributes { get; set; }
    public DbSet<ProductAttribute> ProductAttributes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

    }
}