using api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    // Alle modeller skrives her

    public DbSet<User> Users { get; set; }
    public DbSet<Page> Pages { get; set; }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Page>()
    //         .HasData(
    //             new Page
    //             {
    //                 Title = "Java Title",
    //                 Content = "Java",
    //                 Language = "en",
    //                 Url = "https://en.wikipedia.org/wiki/Java_(programming_language)"
    //             },
    //             new Page
    //             {
    //                 Title = "Javascript Title",
    //                 Content = "Javascript",
    //                 Language = "en",
    //                 Url = "https://en.wikipedia.org/wiki/JavaScript"
    //             }
    //         );
    //
    //     modelBuilder.Entity<User>()
    //         .HasData(
    //             new User
    //             {
    //                 Id = 1,
    //                 Email = "test@test.com",
    //                 Username = "admin",
    //                 Password = "admin"
    //             }
    //         );
    //     base.OnModelCreating(modelBuilder);
    // }
}