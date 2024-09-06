using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models.Entities;

public class Page
{
    [Key, DatabaseGenerated((DatabaseGeneratedOption.None))] 
    [Column("title")] 
    public string Title { get; set; } = null!;

    [Column("url")]
    public string Url { get; set; } = null!;

    [Column("language")]
    public string Language { get; set; } = null!;

    [Column("content")]
    public string Content { get; set; } = null!;
    
    // https://www.entityframeworktutorial.net/faq/set-created-and-modified-date-in-efcore.aspx
    [Column("last_updated")] 
    public DateTime? LastUpdated { get; set; }
}