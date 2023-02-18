using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeInventoryManager.InventoryManager.Models
{
    [Table("productitems")]
    public class ProductItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("productitemid")]
        public int ProductItemId { get; set; } = 0;

        [Required]
        [Column("itembarcodenumber")]
        public string ItemBarcodeNumber { get; set; } = string.Empty;

        [Column("productid")]
        public int ProductId { get; set; } = 0;

        public Product Product { get; set; } = new();
    }
}
