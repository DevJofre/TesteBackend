using System.ComponentModel.DataAnnotations;
using TesteBackend.Models;

namespace TesteBackend.DTOs
{
    public class PostProductAttribute
    {
        [Required(ErrorMessage = "A lista de atributos é obrigatória.")]
        [MinLength(1, ErrorMessage = "A lista de atributos deve conter pelo menos um elemento.")]
        public required List<int> AttributeIds { get; set; }
    }
}
