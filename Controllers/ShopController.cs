using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ThreeAmigosWebApp.Models;
public class ShopController : Controller
{
    private readonly ApplicationDbContext _context;

    public ShopController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(string searchQuery = "")
    {
        var products = _context.Products
            .Where(p => p.Name.Contains(searchQuery) || p.Description.Contains(searchQuery))
            .OrderBy(p => p.Name)
            .ToList();

        return View(products);
    }

    public IActionResult Details(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();

        return View(product);
    }
}
