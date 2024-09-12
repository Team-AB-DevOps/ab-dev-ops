using api.Abstractions;
using api.Models.DTOs;
using api.Models.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
public class PageController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPageRepository _pageRepository;

    public PageController(IPageRepository pageRepository, IMapper mapper)
    {
        _pageRepository = pageRepository;
        _mapper = mapper;
    }


    [Route("/api/search")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PageResponseDto>>> Search([FromQuery] string? q,
        [FromQuery] string? language = "en")
    {
        var pageResults = await _pageRepository.GetByContent(q, language);

        if (pageResults.Count == 0)
        {
            return Ok(new List<PageResponseDto>());
        }

        var pageResultsDto = pageResults
            .Select(page => _mapper.Map<PageResponseDto>(page))
            .ToList();

        return Ok(pageResultsDto);
    }
}