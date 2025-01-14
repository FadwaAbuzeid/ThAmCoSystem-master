namespace ThreeAmigosWebApp.Models
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public int TotalItems => CartItems.Sum(item => item.Quantity);
        public decimal TotalPrice
        {
            get
            {
                return CartItems?.Sum(item => item.Price * item.Quantity) ?? 0;
            }
        }
    }

    public class CartItem
    {
        public int Id { get; set; } 
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity;
    }
}

