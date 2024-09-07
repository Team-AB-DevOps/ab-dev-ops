namespace api.Models.DTOs;

public record LoginRequestDto(
        string Username,
        string Password
    );