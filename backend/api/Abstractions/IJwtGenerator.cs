using api.Models.Entities;

namespace api.Abstractions;

public interface IJwtGenerator
{
    string GenerateToken(User user);
}
