using System.ComponentModel.DataAnnotations.Schema;

namespace HomeInventoryManager.InventoryManager.Models;

[Table("products")]
public class Product
{
    [Column("productid")]
    public int ProductId { get; set; } = 0;

    [Column("productname")]
    public string ProductName { get; set; } = string.Empty;
}