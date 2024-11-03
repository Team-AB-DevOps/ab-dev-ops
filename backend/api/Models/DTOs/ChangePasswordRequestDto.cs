namespace api.Models.DTOs;

public record ChangePasswordRequestDto(string CurrentPassword, string NewPassword, string NewPassword2);