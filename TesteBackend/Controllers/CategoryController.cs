using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
}