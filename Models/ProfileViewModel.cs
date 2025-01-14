namespace ThreeAmigosWebApp.Models;

public class ProfileViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public decimal Funds { get; set; }
    public List<Order> OrderHistory { get; set; } = new List<Order>();
    public bool IsStaff { get; set; }
    public string UserName { get; internal set; }

}
