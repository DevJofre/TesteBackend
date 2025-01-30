using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TesteBackend.DTOs;
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

    [HttpPost(Name = "PostProduct")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Post([FromBody] PostProduct postProduct)
    {
        try
        {
            var product = productService.Create(postProduct);
            string uri = Url.Link("GetProduct", new { id = product.Id });
            return Created(uri, product);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}", Name = "DeleteProduct")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var product = productService.GetById(id);
        if (product == null)
        {
            return NotFound($"Product {id} not found");
        }

        try
        {
            productService.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}", Name = "PatchProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Patch(int id, [FromBody] PatchProduct patchProduct)
    {
        try
        {
            var updatedProduct = productService.Update(id, patchProduct);

            if (updatedProduct == null)
            {
                return NotFound($"Product {id} not found");
            }

            return Ok(updatedProduct);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}