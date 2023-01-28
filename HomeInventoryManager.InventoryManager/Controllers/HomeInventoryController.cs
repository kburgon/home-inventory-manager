using HomeInventoryManager.InventoryManager.Models;
using HomeInventoryManager.InventoryManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventoryManager.InventoryManager.Controllers;

[ApiController]
[Route("/api/inventory")]
public class HomeInventoryController : Controller
{
    private readonly IInventoryManagementService _inventoryManager;

    public HomeInventoryController(IInventoryManagementService inventoryManager)
    {
        _inventoryManager = inventoryManager;
    }

    [HttpGet(Name = "products")]
    public IEnumerable<Product> GetProducts()
    {
        return _inventoryManager.GetProducts();
    }
}