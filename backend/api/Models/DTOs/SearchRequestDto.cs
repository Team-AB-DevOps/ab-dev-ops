namespace api.Models.DTOs;

public record SearchRequestDto(
    string Q,
    string? Language = null
    );