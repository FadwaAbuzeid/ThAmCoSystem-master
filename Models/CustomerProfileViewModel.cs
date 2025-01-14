using Microsoft.AspNetCore.Identity;
namespace ThreeAmigosWebApp.Models;
public class CustomerProfileViewModel
{
    public User User { get; set; }
    public List<Order> Orders { get; set; }
}
