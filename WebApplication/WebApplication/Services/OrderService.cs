using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication.DTO.OrderDTO;
using WebApplication.Infrastructure;
using WebApplication.Interfaces;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly WebApplicationDbContext _dbContext;
        private double dostava = 200;
        public OrderService(IMapper mapper, WebApplicationDbContext dbContext) 
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public DisplayOrderDTO NewOrder(NewOrderDTO newOrderDTO)
        {
            if (String.IsNullOrWhiteSpace(newOrderDTO.Address))
            {
                throw new Exception("Delivery address cannot be empty");
            }

            if (newOrderDTO.OrderItems == null || newOrderDTO.OrderItems.Count() == 0)
            {
                throw new Exception("No items ordered!");
            }

            User customer = _dbContext.Users.Find(newOrderDTO.CustomerId);
            if (customer == null)
            {
                throw new Exception("Id not found!");
            }

            Order order = _mapper.Map<Order>(newOrderDTO);
            order.Customer = customer;
            order.IsCanceled = false;
            _dbContext.Orders.Add(order);

            double price = 0;
            foreach (NewOrderItemDTO newOrderItemDTO in newOrderDTO.OrderItems)
            {
                Item item = _dbContext.Items.Find(newOrderItemDTO.ItemId);
                if (item == null)
                {
                    throw new Exception("Item not found!");
                }
                if(item.Amount < newOrderItemDTO.Amount)
                {
                    throw new Exception("Not enough items available!");
                }

                OrderItem orderItem = new OrderItem()
                {
                    ItemId = newOrderItemDTO.ItemId,
                    OrderId = order.Id,
                    Amount = newOrderItemDTO.Amount,
                    Order = order,
                    Item = item
                };
                _dbContext.OrderItems.Add(orderItem);

                item.Amount -= newOrderItemDTO.Amount;
                price += item.Price * newOrderItemDTO.Amount;

            }
            order.OrderTime = DateTime.Now;
            Random rnd = new Random();
            order.DeliveryTime = order.OrderTime.AddHours(rnd.Next(1,48));
            order.Price = price + dostava;
            _dbContext.SaveChanges();

            return _mapper.Map<DisplayOrderDTO>(_dbContext.Orders.Include(o => o.Customer)
                                                 .Include(o => o.OrderItems)
                                                 .ThenInclude(o => o.Item)
                                                 .ThenInclude(p => p.Seller)
                                                 .Where(o => o.Id == order.Id)
                                                 .FirstOrDefault());
        }
        public DisplayOrderDTO CancelOrder(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DisplayOrderDTO> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DisplayOrderDTO> GetDeliveredOrdersFromCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DisplayOrderDTO> GetNewOrdersFromSeller(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DisplayOrderDTO> GetOldOrdersFromSeller(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DisplayOrderDTO> GetOngoingOrdersFromCustomer(int id)
        {
            throw new NotImplementedException();
        }

    }
}
