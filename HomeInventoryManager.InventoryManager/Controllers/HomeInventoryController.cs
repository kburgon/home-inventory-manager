using HomeInventoryManager.InventoryManager.Data;
using HomeInventoryManager.InventoryManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventoryManager.InventoryManager.Controllers;

[ApiController]
[Route("/api/inventory")]
public class HomeInventoryController : Controller
{
    [HttpGet(Name = "products")]
    public async Task<Product> GetProducts()
    {
        return new Product() { ProductName = "test" };
    }
}