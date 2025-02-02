using System.Diagnostics;
using TesteBackend.DTOs;
using TesteBackend.Models;

namespace TesteBackend.Services;

public class ProductService
{
    private readonly TestDbContext _context;
    private readonly LoggerService _loggerService;

    public ProductService(TestDbContext context, LoggerService loggerService)
    {
        _context = context;
        _loggerService = loggerService;
    }

    public IEnumerable<Product> GetAll()
    {
        _loggerService.LogInformation("Buscando todos os produtos.");
        var products = _context.Products.ToList();
        _loggerService.LogInformation($"Retornando {products.Count} produtos.");
        return products;
    }

    public Product? GetById(int id)
    {
        _loggerService.LogInformation($"Buscando produto com ID: {id}");
        var product = _context.Products.FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            _loggerService.LogWarning($"Produto com ID {id} não encontrado.");
        }
        else
        {
            _loggerService.LogInformation($"Produto com ID {id} encontrado.");
        }

        return product;
    }

    public Product Create(PostProduct postProduct)
    {
        _loggerService.LogInformation("Criando novo produto.");

        var product = new Product()
        {
            Name = postProduct.Name,
            Price = postProduct.Price,
            CategoryId = postProduct.CategoryId,
            DateCreated = DateTime.UtcNow
        };

        _context.Products.Add(product);
        _context.SaveChanges();

        _loggerService.LogInformation($"Produto criado com sucesso. ID: {product.Id}");
        return product;
    }

    public void Delete(int id)
    {
        _loggerService.LogInformation($"Tentando deletar produto com ID: {id}");
        var product = _context.Products.Find(id);

        if (product == null)
        {
            _loggerService.LogWarning($"Produto com ID {id} não encontrado para deletar.");
            throw new Exception("Product not found");
        }

        _context.Products.Remove(product);
        _context.SaveChanges();

        _loggerService.LogInformation($"Produto com ID {id} deletado com sucesso.");
    }

    public Product? Update(int id, PatchProduct patchProduct)
    {
        _loggerService.LogInformation($"Tentando atualizar produto com ID: {id}");
        var product = _context.Products.Find(id);

        if (product == null)
        {
            _loggerService.LogWarning($"Produto com ID {id} não encontrado para atualizar.");
            return null;
        }

        if (patchProduct.Name != null)
            product.Name = patchProduct.Name;

        if (patchProduct.Price.HasValue)
            product.Price = patchProduct.Price.Value;

        if (patchProduct.CategoryId.HasValue)
        {
            var categoryExists = _context.Categories.Any(c => c.Id == patchProduct.CategoryId.Value);

            if (!categoryExists)
            {
                _loggerService.LogError($"Categoria com ID {patchProduct.CategoryId.Value} não encontrada.");
                throw new Exception($"Category with ID {patchProduct.CategoryId.Value} not found");
            }

            product.CategoryId = patchProduct.CategoryId.Value;
        }

        _context.SaveChanges();

        _loggerService.LogInformation($"Produto com ID {id} atualizado com sucesso.");
        return product;
    }

    public void SaveManyAttributeProduct(int productId, List<int> attributeIds)
    {
        _loggerService.LogInformation($"Tentando adicionar atributos ao produto com ID: {productId}");
        List<ProductAttribute> buildedProductAttributes = new List<ProductAttribute>();

        foreach (var attributeId in attributeIds)
        {
            var buildAttribute = new ProductAttribute
            {
                AttributesId = attributeId,
                ProductId = productId
            };

            buildedProductAttributes.Add(buildAttribute);
        }

        _context.ProductAttributes.AddRange(buildedProductAttributes);
        _context.SaveChanges();

        _loggerService.LogInformation($"Atributos adicionados ao produto com ID {productId} com sucesso.");
    }
}