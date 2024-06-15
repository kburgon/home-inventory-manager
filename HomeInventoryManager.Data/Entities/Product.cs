using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeInventoryManager.Data.Entities;


[Table("products")]
public class Product
{
    [Column("productid")]
    public int ProductId { get; set; } = 0;

    [Required]
    [Column("productname")]
    public string ProductName { get; set; } = string.Empty;

	[Column("count")]
	public int Count { get; set; } = 0;

	[Column("countwarningtheshold")]
	public int CountWarningThreshold { get; set; } = 0;
}
