using TesteBackend.Models;

namespace TesteBackend.DTOs;

public class PostAttribute
{
    public required string Brand { get; set; }
    public required string Color { get; set; }
    public OrigemProduto Origem { get; set; }
}