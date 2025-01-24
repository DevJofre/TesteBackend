using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TesteBackend.Models;

namespace TesteBackend.Services;

public class ProductService(TestDbContext context)
{
    public IEnumerable<Product> GetAll()
    {
        return context.Products.Include(p => p.Category).ToList().OrderBy(p => p.DateCreated);
    }

    public Product? GetById(int id)
    {
        return context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
    }
}