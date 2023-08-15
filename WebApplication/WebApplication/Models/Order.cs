namespace WebApplication.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public bool IsCanceled { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public int CustomerId { get; set; }
        public User Customer { get; set; }
    }
}
