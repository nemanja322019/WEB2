namespace WebApplication.DTO.OrderDTO
{
    public class NewOrderDTO
    {
        public string Comment { get; set; }
        public string Address { get; set; }
        public int CustomerId { get; set; }
        public IEnumerable<NewOrderItemDTO> OrderItems { get; set; }
    }
}
