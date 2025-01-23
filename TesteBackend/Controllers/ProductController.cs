using Microsoft.AspNetCore.Mvc;
using TesteBackend.Model;

namespace TesteBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    [HttpGet(Name = "GetProducts")]
    public IEnumerable<Product> Get()
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