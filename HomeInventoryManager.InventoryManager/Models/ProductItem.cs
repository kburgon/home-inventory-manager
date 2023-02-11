using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeInventoryManager.InventoryManager.Models
{
    [Table("productitems")]
    public class ProductItem
    {
        [Column("productitemid")]
        public int ProductItemId { get; set; } = 0;

        [Column("productid")]
        public int ProductId { get; set; } = 0;

        [Required]
        [Column("itembarcodenumber")]
        public string ItemBarcodeNumber { get; set; } = string.Empty;

        public Product Product { get; set; } = new();
    }
}
