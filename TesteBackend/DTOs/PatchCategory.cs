using System.ComponentModel.DataAnnotations;

namespace TesteBackend.DTOs
{
    public class PatchCategory
    {
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da categoria deve ter no máximo 100 caracteres.")]
        public string? Name { get; set; }
    }
}