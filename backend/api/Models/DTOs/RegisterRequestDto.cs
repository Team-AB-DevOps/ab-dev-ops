namespace api.Models.DTOs;

public record RegisterRequestDto(string Username, string Email, string Password, string? Password2);
