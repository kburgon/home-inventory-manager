using HomeInventoryManager.InventoryManager.Models;

namespace HomeInventoryManager.InventoryManager.Data.Repositories;

public interface IProductItemRepository
{
    List<ProductItem> GetAll();

    List<ProductItem> GetByProductId(int productId);

    List<ProductItem> GetByItemBarcodeNumber(string itemBarcodeNumber);

    ProductItem GetByProductItemId(int productItemId);

    Task<ProductItem> CreateAsync(ProductItem item);
}