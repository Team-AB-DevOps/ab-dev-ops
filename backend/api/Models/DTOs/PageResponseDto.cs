namespace api.Models.DTOs;

public record PageResponseDto(
    string Title,
    string Url,
    string Language,
    string Content
);