using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TesteBackend.DTOs;
using TesteBackend.Models;

namespace TesteBackend.Services;

public class CategoryService(TestDbContext context)
{
    public IEnumerable<Category> GetAll()
    {
        return context.Categories.ToList().OrderBy(p => p.DateCreated);
    }

    public Category? GetById(int id)
    {
        return context.Categories.FirstOrDefault(p => p.Id == id);
    }

    public Category Create(PostCategory postCategory)
    {
        var category = new Category()
        {
            Name = postCategory.Name,
            DateCreated = DateTime.UtcNow
        };
        context.Categories.Add(category);
        context.SaveChanges();
        return category;
    }
}