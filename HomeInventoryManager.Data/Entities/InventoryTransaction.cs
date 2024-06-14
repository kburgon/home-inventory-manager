using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeInventoryManager.Data.Entities;

[Table("inventorytransactions")]
public class InventoryTransaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("inventorytransactionid")]
    public int InventoryTransactionId { get; set; } = 0;

    [Required]
    [Column("inventoryadjustment")]
    public int InventoryAdjustment { get; set; } = 0;

    [Required]
    [Column("adjustedat")]
    public DateTime AdjustedAt { get; set; } = new();

    [Column("productitemid")]
    public int ProductItemId { get; set; } = 0;

    public Product Product { get; set; } = new();
}
