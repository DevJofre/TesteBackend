using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
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
}