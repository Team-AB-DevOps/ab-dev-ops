using api.Models.Entities;

namespace api.Abstractions;

public interface IUserRepository
{
    Task<User?> GetByUsername(string username);
    Task<User?> GetByEmail(string email);
    Task<User> CreateUser(User user);
    Task<User?> GetById(int id);
    Task SaveChangesAsync();
}