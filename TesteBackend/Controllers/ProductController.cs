using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TesteBackend.Models;
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

    [HttpGet("{id}", Name = "GetProduct")]
    [ProducesResponseType<Product>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(int id)
    {
        var product = productService.GetById(id);
        return product is null ? NotFound($"Product {id} not found") : Ok(product);
    }
}