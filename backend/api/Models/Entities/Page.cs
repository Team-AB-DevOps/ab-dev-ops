using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models.Entities;

[Table("pages")]
[Index(nameof(Title), Name = "Index_Title")]
public class Page
{
	[Key, DatabaseGenerated((DatabaseGeneratedOption.None))]
	[Column("title")]
	[Required]
	public string Title { get; set; }

	[Column("url")]
	[Required]
	public string Url { get; set; }

	[Column("language")]
	[Required]
	public string Language { get; set; }

	[Column("content")]
	[Required]
	public string Content { get; set; }

	// https://www.entityframeworktutorial.net/faq/set-created-and-modified-date-in-efcore.aspx
	[Column("last_updated")]
	public DateTime? LastUpdated { get; set; }
}
