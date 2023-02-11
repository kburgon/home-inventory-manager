using HomeInventoryManager.InventoryManager.Data.Repositories;
using HomeInventoryManager.InventoryManager.Models;
using HotChocolate.Subscriptions;

namespace HomeInventoryManager.InventoryManager.GraphQL;

public class Mutation
{
    public async Task<Product> CreateProduct([Service] IProductRepository productRepository, [Service] ITopicEventSender eventSender, string productName)
    {
        var product = new Product { ProductName = productName };
        var createdProduct = await productRepository.CreateProductAsync(product);
        await eventSender.SendAsync("ProductCreated", createdProduct);
        return createdProduct;
    }

    public async Task<ProductItem> CreateProductItem([Service] IProductItemRepository productItemRepository, [Service] ITopicEventSender eventSender, int productId, string barcodeNumber)
    {
        var matchingProductItems = productItemRepository.GetByProductId(productId);
        if (matchingProductItems.Any(p => p.ItemBarcodeNumber == barcodeNumber))
        {
            return matchingProductItems?.SingleOrDefault(p => p.ItemBarcodeNumber == barcodeNumber) ?? new ProductItem();
        }

        ProductItem item = new() { ProductId = productId, ItemBarcodeNumber = barcodeNumber };
        var createdProductItem = await productItemRepository.CreateAsync(item);
        await eventSender.SendAsync("ProductItemCreated", createdProductItem);
        return createdProductItem;
    }
}