using System.ComponentModel.DataAnnotations;

namespace TesteBackend.DTOs;

public class PostProduct
{
    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome do produto deve ter no máximo 100 caracteres.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "O preço do produto é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço do produto deve ser maior que zero.")]
    public required float Price { get; set; }

    [Required(ErrorMessage = "A categoria é obrigatória.")]
    [Range(1, int.MaxValue, ErrorMessage = "ID da categoria deve ser maior que zero.")]
    public int CategoryId { get; set; }
}
