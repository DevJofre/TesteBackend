using Microsoft.AspNetCore.Mvc;
using TesteBackend.DTOs;
using TesteBackend.Models;
using TesteBackend.Services;

namespace TesteBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController(
    CategoryService categoryService,
    LoggerService loggerService) : ControllerBase
{
    [HttpGet(Name = "GetCategories")]
    public IEnumerable<Category> Get()
    {
        loggerService.LogInformation("Endpoint GetCaategory foi chamado.");
        return categoryService.GetAll();
    }

    [HttpGet("{id}", Name = "GetCategory")]
    [ProducesResponseType<Category>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(int id)
    {
        loggerService.LogInformation($"Buscando categoria com ID: {id}");

        var category = categoryService.GetById(id);

        loggerService.LogWarning($"Categoria com ID {id} não encontrado.");
        return category is null ? NotFound($"Category {id} not found") : Ok(category);
    }

    [HttpPost(Name = "PostCategory")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Post([FromBody] PostCategory postCategory)
    {
        loggerService.LogInformation("Endpoint PostCategory foi chamado.");

        try
        {
            Category? category = categoryService.Create(postCategory);

            if (category == null)
            {
                loggerService.LogError("Erro ao criar categoria: retorno nulo do serviço.");
                return BadRequest("Erro ao criar categoria.");
            }

            string uri = Url.Link("GetCategory", new { id = category.Id }) ?? string.Empty;

            if (string.IsNullOrEmpty(uri))
            {
                loggerService.LogError("Erro ao gerar a URL da categoria.");
                return BadRequest("Erro ao gerar a URL da categoria.");
            }

            loggerService.LogInformation($"Categoria criada com sucesso. ID: {category.Id}");
            return Created(uri, category);
        }
        catch (Exception ex)
        {
            loggerService.LogError($"Erro ao criar categoria: {ex.Message}", ex);
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("{id}", Name = "DeleteCategory")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        loggerService.LogInformation($"Tentando deletar categoria com ID: {id}");
        var category = categoryService.GetById(id);
        if (category == null)
        {
            loggerService.LogWarning($"Categoria com ID {id} não encontrado para deletar.");
            return NotFound($"Category {id} not found");
        }

        try
        {
            categoryService.Delete(id);
            loggerService.LogInformation($"Categoria com ID {id} deletado com sucesso.");
            return NoContent();
        }
        catch (Exception ex)
        {
            loggerService.LogError($"Erro ao deletar categoria com ID {id}: {ex.Message}", ex);
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}", Name = "PatchCategory")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Patch(int id, [FromBody] PatchCategory patchCategory)
    {
        loggerService.LogInformation($"Tentando atualizar categoria com ID: {id}");
        try
        {
            categoryService.Update(id, patchCategory);
            loggerService.LogInformation($"Categoria com ID {id} atualizado com sucesso.");
            return Ok($"Category {id} updated successfully");
        }
        catch (Exception ex)
        {
            loggerService.LogError($"Erro ao atualizar catedoria com ID {id}: {ex.Message}", ex);
            return BadRequest(ex.Message);
        }
    }

}