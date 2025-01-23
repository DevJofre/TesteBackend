namespace TesteBackend.Model;

public class Product
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public required string Name { get; set; }
}