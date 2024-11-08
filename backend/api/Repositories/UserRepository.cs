using api.Abstractions;
using api.Data;
using api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class UserRepository : IUserRepository
{
	private readonly DataContext _dataContext;

	public UserRepository(DataContext dataContext)
	{
		_dataContext = dataContext;
	}

	public async Task<User?> GetByUsername(string username)
	{
		return await _dataContext.Users.FirstOrDefaultAsync(u => u.Username == username);
	}

	public async Task<User?> GetByEmail(string email)
	{
		return await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == email);
	}

	public async Task<User> CreateUser(User user)
	{
		var createdUser = await _dataContext.Users.AddAsync(user);
		await SaveChangesAsync();
		return createdUser.Entity;
	}

	public async Task<User?> GetById(int id)
	{
		return await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == id);
	}

	public async Task SaveChangesAsync()
	{
		await _dataContext.SaveChangesAsync();
	}
}
