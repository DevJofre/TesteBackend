using System.Text.Json.Serialization;

namespace TesteBackend.Models;

public class Product
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public required string Name { get; set; }
    
    /* relations */
    public Category? Category { get; set; }
    [JsonIgnore]
    public int? CategoryId { get; set; }
}