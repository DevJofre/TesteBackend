using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TesteBackend.DTOs;
using TesteBackend.Models;

namespace TesteBackend.Services;

public class CategoryService
{
    private readonly TestDbContext _context;

    public CategoryService(TestDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Category> GetAll()
    {
        return _context.Categories.OrderBy(p => p.DateCreated).ToList();
    }

    public Category? GetById(int id)
    {
        return _context.Categories.FirstOrDefault(p => p.Id == id);
    }

    public Category Create(PostCategory postCategory)
    {
        var category = new Category()
        {
            Name = postCategory.Name,
            DateCreated = DateTime.UtcNow
        };
        _context.Categories.Add(category);
        _context.SaveChanges();
        return category;
    }

    public void Delete(int id)
    {
        var category = _context.Categories.Find(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
        else
        {
            throw new Exception($"Category with ID {id} not found");
        }
    }

    public void Update(int id, PatchCategory patchCategory)
    {
        var category = _context.Categories.Find(id);
        if (category == null)
        {
            throw new Exception($"Category with ID {id} not found");
        }

        // Atualiza apenas os campos fornecidos no PATCH
        if (!string.IsNullOrEmpty(patchCategory.Name))
        {
            category.Name = patchCategory.Name;
        }

        _context.SaveChanges();
    }
}
