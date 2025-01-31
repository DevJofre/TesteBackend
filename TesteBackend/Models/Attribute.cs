using System.Text.Json.Serialization;

namespace TesteBackend.Models;

public class Attribute
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public required string Brand { get; set; }
    public required string Color { get; set; }
    public OrigemProduto Origem { get; set; }

}

public enum OrigemProduto
{
    Nacional,
    Importado
}