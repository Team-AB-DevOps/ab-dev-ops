using api.Models.Entities;

namespace api.Abstractions;

public interface IPageRepository
{
    Task<IEnumerable<Page?>> GetByContent(string? q, string? language);
}