using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public ActionResult<IEnumerable<AttributeModel>> GetAll()
        {
            loggerService.LogInformation("Endpoint GetAll de Atributos foi chamado.");
            var attributes = _attributeService.GetAll();
            loggerService.LogInformation($"Retornando {attributes.Count()} atributos.");
            return Ok(attributes);
        }

        [HttpGet("{id}")]
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
        public ActionResult<AttributeModel> Create(AttributeModel attribute)
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

        [HttpPut("{id}")]
        public IActionResult Update(int id, AttributeModel updatedAttribute)
        {
            loggerService.LogInformation($"Tentando atualizar atributo com ID: {id}");

            try
            {
                _attributeService.Update(id, updatedAttribute);
                loggerService.LogInformation($"Atributo com ID {id} atualizado com sucesso.");
                return NoContent();
            }
            catch (Exception ex)
            {
                loggerService.LogError($"Erro ao atualizar atributo com ID {id}: {ex.Message}", ex);
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
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
                return NotFound(ex.Message);
            }
        }
    }
}