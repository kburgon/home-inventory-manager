namespace HomeInventoryManager.InventoryManager.Models
{
	public class ProductItem
	{
		public int ProductItemId { get; set; } = 0;

		public int ProductId { get; set; } = 0;

		public string ItemBarcodeNumber { get; set; } = string.Empty;
	}
}
