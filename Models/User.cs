using Microsoft.AspNetCore.Identity;
namespace ThreeAmigosWebApp.Models;

public class User : IdentityUser
{
    public string Address { get; set; }
    public new string PhoneNumber { get; set; }
    public decimal Funds { get; set; }
}
