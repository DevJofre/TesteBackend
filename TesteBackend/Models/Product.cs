namespace TesteBackend.Models;

public class Product
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public required string Name { get; set; }
}