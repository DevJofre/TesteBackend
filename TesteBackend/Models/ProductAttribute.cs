using System.Text.Json.Serialization;

namespace TesteBackend.Models;

public class ProductAttribute
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }

    public Attribute? Attribute { get; set; }
    [JsonIgnore]
    public int? AttributesId { get; set; }

    public Product? Product { get; set; }
    [JsonIgnore]
    public int? ProductId { get; set; }
}