namespace ThreeAmigosWebApp.Models
{
    public class CheckoutViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AvailableFunds { get; set; }
    }
}
