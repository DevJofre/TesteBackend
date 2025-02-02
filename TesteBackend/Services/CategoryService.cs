using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TesteBackend.DTOs;
using TesteBackend.Models;

namespace TesteBackend.Services;

public class CategoryService
{
    private readonly TestDbContext _context;
    private readonly LoggerService loggerService;

    public CategoryService(TestDbContext context, LoggerService loggerService)
    {
        _context = context;
        this.loggerService = loggerService;
    }

    public IEnumerable<Category> GetAll()
    {
        loggerService.LogInformation("Buscando todas as categorias.");
        var categories = _context.Categories.OrderBy(p => p.DateCreated).ToList();
        loggerService.LogInformation($"Retornando {categories.Count} categorias.");
        return categories;
    }

    public Category? GetById(int id)
    {
        loggerService.LogInformation($"Buscando categoria com ID: {id}");
        var category = _context.Categories.FirstOrDefault(p => p.Id == id);

        if (category == null)
        {
            loggerService.LogWarning($"Categoria com ID {id} não encontrada.");
        }
        else
        {
            loggerService.LogInformation($"Categoria com ID {id} encontrada.");
        }

        return category;
    }

    public Category Create(PostCategory postCategory)
    {
        loggerService.LogInformation("Criando nova categoria.");

        var category = new Category()
        {
            Name = postCategory.Name,
            DateCreated = DateTime.UtcNow
        };

        _context.Categories.Add(category);
        _context.SaveChanges();

        loggerService.LogInformation($"Categoria criada com sucesso. ID: {category.Id}");
        return category;
    }

    public void Delete(int id)
    {
        loggerService.LogInformation($"Tentando deletar categoria com ID: {id}");
        var category = _context.Categories.Find(id);

        if (category == null)
        {
            loggerService.LogWarning($"Categoria com ID {id} não encontrada para deletar.");
            throw new Exception("Category not found");
        }

        var productsWithCategory = _context.Products.Where(p => p.CategoryId == id).ToList();

        foreach (var product in productsWithCategory)
        {
            product.CategoryId = null;
        }

        _context.SaveChanges();

        _context.Categories.Remove(category);
        _context.SaveChanges();

        loggerService.LogInformation($"Categoria com ID {id} deletada com sucesso.");
    }

    public void Update(int id, PatchCategory patchCategory)
    {
        loggerService.LogInformation($"Tentando atualizar categoria com ID: {id}");
        var category = _context.Categories.Find(id);

        if (category == null)
        {
            loggerService.LogWarning($"Categoria com ID {id} não encontrada para atualizar.");
            throw new Exception($"Category with ID {id} not found");
        }

        if (!string.IsNullOrEmpty(patchCategory.Name))
        {
            category.Name = patchCategory.Name;
        }

        _context.SaveChanges();

        loggerService.LogInformation($"Categoria com ID {id} atualizada com sucesso.");
    }
}