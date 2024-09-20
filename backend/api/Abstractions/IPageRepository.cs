using api.Models.Entities;

namespace api.Abstractions;

public interface IPageRepository
{
	Task<List<Page>> GetByContent(string? q, string? language);
}
