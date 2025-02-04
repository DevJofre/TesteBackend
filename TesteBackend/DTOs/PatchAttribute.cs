using TesteBackend.Models;

namespace TesteBackend.DTOs;

public class PatchAttribute
{
    public string? Brand { get; set; }
    public string? Color { get; set; }
    public OrigemProduto? Origem { get; set; }
}