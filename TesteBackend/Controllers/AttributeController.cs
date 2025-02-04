using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TesteBackend.DTOs;
using TesteBackend.Services;
using AttributeModel = TesteBackend.Models.Attribute;

namespace TesteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        private readonly AttributeService _attributeService;
        private readonly LoggerService loggerService;

        public AttributeController(AttributeService attributeService, LoggerService loggerService)
        {
            _attributeService = attributeService;
            this.loggerService = loggerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<AttributeModel>> GetAll()
        {
            loggerService.LogInformation("Endpoint GetAll de Atributos foi chamado.");
            var attributes = _attributeService.GetAll();
            loggerService.LogInformation($"Retornando {attributes.Count()} atributos.");
            return Ok(attributes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AttributeModel> GetById(int id)
        {
            loggerService.LogInformation($"Buscando atributo com ID: {id}");

            var attribute = _attributeService.GetById(id);
            if (attribute == null)
            {
                loggerService.LogWarning($"Atributo com ID {id} não encontrado.");
                return NotFound();
            }

            loggerService.LogInformation($"Atributo com ID {id} retornado com sucesso.");
            return Ok(attribute);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] PostAttribute attribute)
        {
            loggerService.LogInformation("Endpoint Create de Atributo foi chamado.");

            try
            {
                var createdAttribute = _attributeService.Create(attribute);
                loggerService.LogInformation($"Atributo criado com sucesso. ID: {createdAttribute.Id}");
                return CreatedAtAction(nameof(GetById), new { id = createdAttribute.Id }, createdAttribute);
            }
            catch (Exception ex)
            {
                loggerService.LogError($"Erro ao criar atributo: {ex.Message}", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Patch(int id, [FromBody] PatchAttribute patchDoc)
        {
            loggerService.LogInformation($"Tentando atualizar parcialmente o atributo com ID: {id}");

            if (patchDoc == null)
            {
                loggerService.LogWarning("O documento de patch recebido é nulo.");
                return BadRequest("O documento de patch não pode ser nulo.");
            }

            var attribute = _attributeService.GetById(id);
            if (attribute == null)
            {
                loggerService.LogWarning($"Atributo com ID {id} não encontrado.");
                return NotFound();
            }

            try
            {
                if (patchDoc.Brand != null)
                {
                    attribute.Brand = patchDoc.Brand;
                }
                if (patchDoc.Color != null)
                {
                    attribute.Color = patchDoc.Color;
                }
                attribute.Origem = patchDoc.Origem;

                if (!TryValidateModel(attribute))
                {
                    loggerService.LogWarning("Modelo inválido após a aplicação do patch.");
                    return BadRequest(ModelState);
                }

                _attributeService.Update(id, attribute);
                loggerService.LogInformation($"Atributo com ID {id} atualizado parcialmente com sucesso.");
                return NoContent();
            }
            catch (Exception ex)
            {
                loggerService.LogError($"Erro ao atualizar parcialmente o atributo com ID {id}: {ex.Message}", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            loggerService.LogInformation($"Tentando deletar atributo com ID: {id}");

            try
            {
                _attributeService.Delete(id);
                loggerService.LogInformation($"Atributo com ID {id} deletado com sucesso.");
                return NoContent();
            }
            catch (Exception ex)
            {
                loggerService.LogError($"Erro ao deletar atributo com ID {id}: {ex.Message}", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}