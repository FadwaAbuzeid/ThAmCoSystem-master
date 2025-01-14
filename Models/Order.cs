using System;
using System.Collections.Generic;

namespace ThreeAmigosWebApp.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public bool IsDispatched { get; set; }
        public DateTime? DispatchedDate { get; set; }
        public List<OrderItem> OrderItems { get; set; }
}

}
