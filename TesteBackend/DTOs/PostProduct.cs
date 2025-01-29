namespace TesteBackend.DTOs;

public class PostProduct
{
    public required string Name { get; set; }
    public required float Price { get; set; }
    public int CategoryId { get; set; }
}
