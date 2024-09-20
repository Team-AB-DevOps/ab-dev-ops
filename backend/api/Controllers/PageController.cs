using api.Abstractions;
using api.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
public class PageController : ControllerBase
{
    private readonly IPageRepository _pageRepository;

    public PageController(IPageRepository pageRepository)
    {
        _pageRepository = pageRepository;
    }

    [Route("/api/search")]
    [HttpPost]
    public async Task<ActionResult<IEnumerable<PageResponseDto>>> Search(
        [FromBody] SearchRequestDto searchRequest
    )
    {
        var language = searchRequest.Language ?? "en";

        var pageResults = await _pageRepository.GetByContent(searchRequest.Q, language);

        if (pageResults.Count == 0)
        {
            return Ok(new List<PageResponseDto>());
        }

        var pageResultsDto = pageResults
            .Select(page => new PageResponseDto(page.Title, page.Url, page.Language, page.Content))
            .ToList();

        return Ok(pageResultsDto);
    }
}
