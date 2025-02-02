using System.Text.Json.Serialization;
using TesteBackend.Models;

namespace TesteBackend.Models;

public class Attribute
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public required string Brand { get; set; }
    public required string Color { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrigemProduto Origem { get; set; }

}

public enum OrigemProduto
{
    Nacional,
    Importado
}