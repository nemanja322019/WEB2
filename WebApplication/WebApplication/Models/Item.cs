namespace WebApplication.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public int SellerId { get; set; }
        public User Seller { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string Image { get; set; }
    }
}
