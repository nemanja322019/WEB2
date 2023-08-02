namespace WebApplication.Models
{
    public class Seller : User
    {
        public List<Item> Items { get; set; }
    }
}
