using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThreeAmigosWebApp.Models;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var featuredProducts = _context.Products
                                       .OrderByDescending(p => p.Id)
                                       .Take(5)
                                       .ToList();

        var bestSellers = _context.Products
                                  .Where(p => p.StockQuantity > 10)
                                  .OrderBy(p => p.Price)
                                  .Take(5)
                                  .ToList();

        var newArrivals = _context.Products
                                 .OrderByDescending(p => p.Id)
                                 .Take(5)
                                 .ToList();

        var model = new HomeViewModel
        {
            FeaturedProducts = featuredProducts,
            BestSellers = bestSellers,
            NewArrivals = newArrivals
        };

        return View(model);
    }


    public IActionResult About()
    {
        return View();
    }




}
