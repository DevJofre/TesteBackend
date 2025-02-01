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
        private readonly LoggerService _loggerService;

        public AttributeController(AttributeService attributeService, LoggerService loggerService)
        {
            _attributeService = attributeService;
            _loggerService = loggerService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AttributeModel>> GetAll()
        {
            _loggerService.LogInformation("Endpoint GetAll de Atributos foi chamado.");
            var attributes = _attributeService.GetAll();
            _loggerService.LogInformation($"Retornando {attributes.Count()} atributos.");
            return Ok(attributes);
        }

        [HttpGet("{id}")]
        public ActionResult<AttributeModel> GetById(int id)
        {
            _loggerService.LogInformation($"Buscando atributo com ID: {id}");

            var attribute = _attributeService.GetById(id);
            if (attribute == null)
            {
                _loggerService.LogWarning($"Atributo com ID {id} não encontrado.");
                return NotFound();
            }

            _loggerService.LogInformation($"Atributo com ID {id} retornado com sucesso.");
            return Ok(attribute);
        }

        [HttpPost]
        public ActionResult<AttributeModel> Create(AttributeModel attribute)
        {
            _loggerService.LogInformation("Endpoint Create de Atributo foi chamado.");

            try
            {
                var createdAttribute = _attributeService.Create(attribute);
                _loggerService.LogInformation($"Atributo criado com sucesso. ID: {createdAttribute.Id}");
                return CreatedAtAction(nameof(GetById), new { id = createdAttribute.Id }, createdAttribute);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Erro ao criar atributo: {ex.Message}", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, AttributeModel updatedAttribute)
        {
            _loggerService.LogInformation($"Tentando atualizar atributo com ID: {id}");

            try
            {
                _attributeService.Update(id, updatedAttribute);
                _loggerService.LogInformation($"Atributo com ID {id} atualizado com sucesso.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Erro ao atualizar atributo com ID {id}: {ex.Message}", ex);
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _loggerService.LogInformation($"Tentando deletar atributo com ID: {id}");

            try
            {
                _attributeService.Delete(id);
                _loggerService.LogInformation($"Atributo com ID {id} deletado com sucesso.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Erro ao deletar atributo com ID {id}: {ex.Message}", ex);
                return NotFound(ex.Message);
            }
        }
    }
}