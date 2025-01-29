using System.Diagnostics;
using TesteBackend.DTOs;
using TesteBackend.Models;

namespace TesteBackend.Services;

public class ProductService
{
    private readonly TestDbContext _context;

    public ProductService(TestDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetAll()
    {
        return _context.Products.ToList();
    }

    public Product? GetById(int id)
    {
        return _context.Products.FirstOrDefault(p => p.Id == id);
    }

    public Product Create(PostProduct postProduct)
    {
        var product = new Product()
        {
            Name = postProduct.Name,
            Price = postProduct.Price,
            CategoryId = postProduct.CategoryId,
            DateCreated = DateTime.UtcNow
        };

        _context.Products.Add(product);
        _context.SaveChanges();
        return product;
    }
}
