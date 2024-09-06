using api.Models.DTOs;
using api.Models.Entities;

namespace api.Mappers;

public static class UserMapper
{
    public static UserResponseDto ToDto(this User user)
    {
        return new UserResponseDto(
            user.Username,
            user.Email
        );
    }
}