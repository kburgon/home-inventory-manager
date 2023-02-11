using HomeInventoryManager.InventoryManager.Data.Repositories;
using HomeInventoryManager.InventoryManager.Models;

namespace HomeInventoryManager.InventoryManager.GraphQL
{
    public class Query
    {
        public List<Product> GetAllProducts([Service] ProductRepository productRepository)
            => productRepository.GetProducts();

        public List<ProductItem> GetAllProductItems([Service] ProductItemRepository productItemRepository)
            => productItemRepository.GetAll();

        public List<ProductItem> GetProductItemsByProductId([Service] ProductItemRepository productItemRepository, int productId)
            => productItemRepository.GetByProductId(productId);

        public List<ProductItem> GetProductItemsByBarcodeNumber([Service] ProductItemRepository productItemRepository, string itemBarcodeNumber)
            => productItemRepository.GetByItemBarcodeNumber(itemBarcodeNumber);
    }
}