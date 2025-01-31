using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TesteBackend.Services;
using AttributeModel = TesteBackend.Models.Attribute; // Alias para evitar conflito

namespace TesteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        private readonly AttributeService _attributeService;

        public AttributeController(AttributeService attributeService)
        {
            _attributeService = attributeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AttributeModel>> GetAll()
        {
            return Ok(_attributeService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<AttributeModel> GetById(int id)
        {
            var attribute = _attributeService.GetById(id);
            if (attribute == null)
            {
                return NotFound();
            }
            return Ok(attribute);
        }

        [HttpPost]
        public ActionResult<AttributeModel> Create(AttributeModel attribute)
        {
            var createdAttribute = _attributeService.Create(attribute);
            return CreatedAtAction(nameof(GetById), new { id = createdAttribute.Id }, createdAttribute);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, AttributeModel updatedAttribute)
        {
            try
            {
                _attributeService.Update(id, updatedAttribute);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _attributeService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
