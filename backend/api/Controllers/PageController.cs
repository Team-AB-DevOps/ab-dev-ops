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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PageResponseDto>>> Search([FromQuery] string? q,
        [FromQuery] string? language = "en")
    {
        var pageResults = await _pageRepository.GetByContent(q, language);
        // var pageResultList = pageResults.ToList();

        var toDto = new List<PageResponseDto>();

        if (pageResults.Any())
        {
            return Ok(toDto);
        }

        foreach (var page in pageResults)
        {
            if (page == null)
            {
                continue;
            }

            var pageDto = new PageResponseDto(page.Title, page.Url, page.Language, page.Content);
            toDto.Add(pageDto);
        }

        return Ok(toDto);
    }
}