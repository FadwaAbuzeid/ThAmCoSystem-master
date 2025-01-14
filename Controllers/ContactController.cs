using Microsoft.AspNetCore.Mvc;

public class ContactController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Submit(string name, string email, string message)
    {
        TempData["Message"] = "Thank you for contacting us!";
        return RedirectToAction("Index");
    }
}
