using Microsoft.AspNetCore.Mvc;
using TesteBackend.DTOs;
using TesteBackend.Models;
using TesteBackend.Services;

namespace TesteBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController(CategoryService categoryService) : ControllerBase
{
    [HttpGet(Name = "GetCategories")]
    public IEnumerable<Category> Get()
    {
        return categoryService.GetAll();
    }

    [HttpGet("{id}", Name = "GetCategory")]
    [ProducesResponseType<Product>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(int id)
    {
        var category = categoryService.GetById(id);
        return category is null ? NotFound($"Category {id} not found") : Ok(category);
    }

    [HttpPost(Name = "PostCategory")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Post([FromBody] PostCategory postCategory)
    {
        try
        {
            var category = categoryService.Create(postCategory);
            string uri = Url.Link("GetCategory", new { id = category.Id });
            return Created(uri, category);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}