using api.Abstractions;
using api.Data;
using api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class PageRepository : IPageRepository
{
    private readonly DataContext _context;

    public PageRepository(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<Page?>> GetByContent(string? q, string? language)
    {
        if (!string.IsNullOrWhiteSpace(q))
        {
            return await _context.Pages
                .Where(p => p.Content.Contains(q) && p.Language.Equals(language))
                .ToListAsync();
        }

        return new List<Page?>();
    }
}