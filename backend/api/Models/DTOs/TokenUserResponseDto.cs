namespace api.Models.DTOs;

public record TokenUserResponseDto(
    string Token,
    UserResponseDto User
    );