using Microsoft.AspNetCore.Mvc;
using TesteBackend.Model;
using TesteBackend.Services;

namespace TesteBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(ProductService productService) : ControllerBase
{
    [HttpGet(Name = "GetProducts")]
    public IEnumerable<Product> Get()
    {
        return productService.GetAll();
    }
}