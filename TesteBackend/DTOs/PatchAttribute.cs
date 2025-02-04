using System.ComponentModel.DataAnnotations;
using TesteBackend.Models;

namespace TesteBackend.DTOs
{
    public class PatchAttribute
    {
        [Required(ErrorMessage = "A marca é obrigatória.")]
        [StringLength(50, ErrorMessage = "A marca deve ter no máximo 50 caracteres.")]
        public string? Brand { get; set; }

        [Required(ErrorMessage = "A cor é obrigatória.")]
        [StringLength(30, ErrorMessage = "A cor deve ter no máximo 30 caracteres.")]
        public string? Color { get; set; }

        [Required(ErrorMessage = "A origem do produto é obrigatória.")]
        [EnumDataType(typeof(OrigemProduto), ErrorMessage = "A origem do produto deve ser Nacional ou Importado.")]
        public OrigemProduto Origem { get; set; }
    }
}
