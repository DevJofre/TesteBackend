using System.Diagnostics;
using TesteBackend.Models;

namespace TesteBackend.Services;

public class ProductService
{
    public IEnumerable<Product> GetAll()
    {
        return Enumerable.Range(1, 5).Select(index => new Product
            {
                Id = index,
                Date = DateTime.Now.AddDays(index),
                Name = $"Product {index}",
            })
            .ToArray();
    }
}