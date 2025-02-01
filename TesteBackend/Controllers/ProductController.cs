using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TesteBackend.DTOs;
using TesteBackend.Models;
using TesteBackend.Services;

namespace TesteBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(
    ProductService productService,
    AttributeService attributeService,
    LoggerService loggerService) : ControllerBase
{
    [HttpGet(Name = "GetProducts")]
    public IEnumerable<Product> Get()
    {
        loggerService.LogInformation("Endpoint GetProducts foi chamado.");
        return productService.GetAll();
    }

    [HttpGet("{id}", Name = "GetProduct")]
    [ProducesResponseType<Product>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(int id)
    {
        loggerService.LogInformation($"Buscando produto com ID: {id}");

        var product = productService.GetById(id);
        if (product is null)
        {
            loggerService.LogWarning($"Produto com ID {id} não encontrado.");
            return NotFound($"Product {id} not found");
        }

        var attributes = attributeService.GetAttributesByProductId(id);
        product.Attributes = [.. attributes];

        loggerService.LogInformation($"Produto com ID {id} retornado com sucesso.");
        return Ok(product);
    }

    [HttpPost(Name = "PostProduct")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Post([FromBody] PostProduct postProduct)
    {
        loggerService.LogInformation("Endpoint PostProduct de Produto foi chamado.");

        try
        {
            var product = productService.Create(postProduct);
            string uri = Url.Link("GetProduct", new { id = product.Id });

            loggerService.LogInformation($"Produto criado com sucesso. ID: {product.Id}");
            return Created(uri, product);
        }
        catch (Exception ex)
        {
            loggerService.LogError($"Erro ao criar produto: {ex.Message}", ex);
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}", Name = "DeleteProduct")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        loggerService.LogInformation($"Tentando deletar produto com ID: {id}");

        var product = productService.GetById(id);
        if (product == null)
        {
            loggerService.LogWarning($"Produto com ID {id} não encontrado para deletar.");
            return NotFound($"Product {id} not found");
        }

        try
        {
            productService.Delete(id);
            loggerService.LogInformation($"Produto com ID {id} deletado com sucesso.");
            return NoContent();
        }
        catch (Exception ex)
        {
            loggerService.LogError($"Erro ao deletar produto com ID {id}: {ex.Message}", ex);
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}", Name = "PatchProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Patch(int id, [FromBody] PatchProduct patchProduct)
    {
        loggerService.LogInformation($"Tentando atualizar produto com ID: {id}");

        try
        {
            var updatedProduct = productService.Update(id, patchProduct);

            if (updatedProduct == null)
            {
                loggerService.LogWarning($"Produto com ID {id} não encontrado para atualizar.");
                return NotFound($"Product {id} not found");
            }

            loggerService.LogInformation($"Produto com ID {id} atualizado com sucesso.");
            return Ok(updatedProduct);
        }
        catch (Exception ex)
        {
            loggerService.LogError($"Erro ao atualizar produto com ID {id}: {ex.Message}", ex);
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("attributes/{id}", Name = "AddAttributesToProduct")]
    [ProducesResponseType<Product>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddAttributes(int id, [FromBody] PostAttribute addAttributes)
    {
        loggerService.LogInformation($"Tentando adicionar atributos ao produto com ID: {id}");

        try
        {
            var product = productService.GetById(id);

            if (product == null)
            {
                loggerService.LogWarning($"Produto com ID {id} não encontrado para adicionar atributos.");
                return NotFound($"Product {id} not found");
            }

            var foundedAttributes = attributeService.GetByIds(addAttributes.AttributeIds);
            productService.SaveManyAttributeProduct(id, addAttributes.AttributeIds);

            loggerService.LogInformation($"Atributos adicionados ao produto com ID {id} com sucesso.");
            return NoContent();
        }
        catch (Exception ex)
        {
            loggerService.LogError($"Erro ao adicionar atributos ao produto com ID {id}: {ex.Message}", ex);
            return BadRequest(ex.Message);
        }
    }
}