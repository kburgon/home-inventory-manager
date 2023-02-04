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
}