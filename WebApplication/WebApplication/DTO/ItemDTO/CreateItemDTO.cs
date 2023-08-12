namespace WebApplication.DTO.ItemDTO
{
    public class CreateItemDTO
    {
        public int SellerId { get; set; }
        public string ItemName { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
    }
}
