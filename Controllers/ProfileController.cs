using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ThreeAmigosWebApp.Models;
using System.Threading.Tasks;

public class ProfileController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public ProfileController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        return View(user); 
    }

    [HttpPost]
    public async Task<IActionResult> Index(User model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByIdAsync(model.Id);
        if (user == null)
        {
            return NotFound();
        }

        if (user.UserName != model.UserName)
        {
            user.UserName = model.UserName;
        }
        user.Email = model.Email;
        user.Address = model.Address;
        user.PhoneNumber = model.PhoneNumber;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model); 
        }

        await _signInManager.RefreshSignInAsync(user);

        TempData["SuccessMessage"] = "Profile updated successfully!";
        return RedirectToAction("Index");
    }
}
