using WebApplication.DTO.ItemDTO;

namespace WebApplication.DTO.OrderDTO
{
    public class DisplayOrderDTO
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public bool IsCanceled { get; set; }
        public string Status { get; set; }
        public List<DisplayItemDTO> OrderItems { get; set; }
    }
}
