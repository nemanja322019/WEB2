using WebApplication.DTO.OrderDTO;

namespace WebApplication.Interfaces
{
    public interface IOrderService
    {
        public DisplayOrderDTO NewOrder(NewOrderDTO newOrderDTO);
        public DisplayOrderDTO CancelOrder(int id);
        public IEnumerable<DisplayOrderDTO> GetAllOrders();
        public IEnumerable<DisplayOrderDTO> GetNewOrdersFromSeller(int id);
        public IEnumerable<DisplayOrderDTO> GetOldOrdersFromSeller(int id);
        public IEnumerable<DisplayOrderDTO> GetDeliveredOrdersFromCustomer(int id);
        public IEnumerable<DisplayOrderDTO> GetOngoingOrdersFromCustomer(int id);
    }
}
