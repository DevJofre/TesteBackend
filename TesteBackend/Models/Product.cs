using System.Text.Json.Serialization;

namespace TesteBackend.Models;

public class Product
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public required string Name { get; set; }
    public float Price { get; set; }
    public List<Attribute> Attributes { get; set; } = new();

    /* relations */
    public Category? Category { get; set; }
    [JsonIgnore]
    public int? CategoryId { get; set; }
}