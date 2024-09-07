using api.Models.DTOs;
using api.Models.Entities;
using AutoMapper;

namespace api.Profiles;

public class PageProfile : Profile
{
    public PageProfile()
    {
        CreateMap<Page, PageResponseDto>();
    }
}