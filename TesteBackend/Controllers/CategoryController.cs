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

    [HttpDelete("{id}", Name = "DeleteCategory")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var category = categoryService.GetById(id);
        if (category == null)
        {
            return NotFound($"Category {id} not found");
        }

        try
        {
            categoryService.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}", Name = "PatchCategory")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Patch(int id, [FromBody] PatchCategory patchCategory)
    {
        try
        {
            categoryService.Update(id, patchCategory);
            return Ok($"Category {id} updated successfully");
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

}