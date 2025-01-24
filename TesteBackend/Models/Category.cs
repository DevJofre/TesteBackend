using System.Text.Json.Serialization;

namespace TesteBackend.Models;

public class Category
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public required string Name { get; set; }
    
    /* relations */
    [JsonIgnore]
    public ICollection<Product> Products { get; set; } = new List<Product>();
}