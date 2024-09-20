using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models.Entities;

[Table("users")]
[Index(nameof(Username), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class User
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	[Required]
	[Column("username")]
	public string Username { get; set; }

	[Required]
	[Column("email")]
	[EmailAddress]
	public string Email { get; set; }

	[Required]
	[Column("password")]
	public string Password { get; set; }
}
