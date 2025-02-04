using System.ComponentModel.DataAnnotations;

namespace TesteBackend.DTOs;

public class PostCategory
{
    [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome da categoria deve ter no máximo 100 caracteres.")]
    public required string Name { get; set; }
}