using System.ComponentModel.DataAnnotations;

namespace TesteBackend.DTOs;
public class PatchProduct
{
    [StringLength(100, ErrorMessage = "O nome do produto deve ter no máximo 100 caracteres.")]
    public string? Name { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "O preço do produto deve ser maior que zero.")]
    public float? Price { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "ID da categoria deve ser maior que zero.")]
    public int? CategoryId { get; set; }
}