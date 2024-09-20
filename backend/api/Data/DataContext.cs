using api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class DataContext : DbContext
{
	public DataContext(DbContextOptions<DataContext> options)
		: base(options) { }

	// Alle modeller skrives her

	public DbSet<User> Users { get; set; }
	public DbSet<Page> Pages { get; set; }
}
