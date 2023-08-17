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

            return _mapper.Map<DisplayOrderDTO>(ExtractOrderData(order));
        }
        public DisplayOrderDTO CancelOrder(int id)
        {
            Order order = _dbContext.Orders.Find(id);
            if(order == null)
            {
                throw new Exception("Order id not found!");
            }

            if(order.OrderTime.AddHours(1) < DateTime.Now)
            {
                throw new Exception("Too late to cancel the order!");
            }

            ExtractOrderData(order);
            order.IsCanceled = true;
            foreach(OrderItem orderedItem in order.OrderItems)
            {
                orderedItem.Item.Amount += orderedItem.Amount;
            }

            _dbContext.SaveChanges();

            return _mapper.Map<DisplayOrderDTO>(order);
        }

        public IEnumerable<DisplayOrderDTO> GetAllOrders()
        {
            IEnumerable<Order> orders = _dbContext.Orders;
            foreach(Order order in orders)
            {
                ExtractOrderData(order);
            }
            return _mapper.Map<IEnumerable<DisplayOrderDTO>>(orders);
        }

        public IEnumerable<DisplayOrderDTO> GetDeliveredOrdersFromCustomer(int id)
        {
            IEnumerable<Order> orders = _dbContext.Orders.Where(o => o.Customer.Id == id && o.DeliveryTime < DateTime.Now && !o.IsCanceled);
            foreach (Order order in orders)
            {
                ExtractOrderData(order);
            }
            return _mapper.Map<IEnumerable<DisplayOrderDTO>>(orders);
        }

        public IEnumerable<DisplayOrderDTO> GetOngoingOrdersFromCustomer(int id)
        {
            IEnumerable<Order> orders = _dbContext.Orders.Where(o => o.Customer.Id == id && o.DeliveryTime > DateTime.Now && !o.IsCanceled);
            foreach (Order order in orders)
            {
                ExtractOrderData(order);
            }
            return _mapper.Map<IEnumerable<DisplayOrderDTO>>(orders);
        }

        public IEnumerable<DisplayOrderDTO> GetNewOrdersFromSeller(int id)
        {
            List<Order> orders = _dbContext.Orders.Where(o => o.DeliveryTime > DateTime.Now && !o.IsCanceled).ToList();
            foreach (Order order in orders)
            {
                ExtractOrderData(order);
            }
            orders = orders.FindAll(o => o.OrderItems.FindAll(oi => oi.Item.SellerId == id).Count != 0);
            return _mapper.Map<IEnumerable<DisplayOrderDTO>>(orders);
        }

        public IEnumerable<DisplayOrderDTO> GetOldOrdersFromSeller(int id)
        {
            List<Order> orders = _dbContext.Orders.Where(o => o.DeliveryTime < DateTime.Now).ToList();
            foreach (Order order in orders)
            {
                ExtractOrderData(order);
            }
            orders = orders.FindAll(o => o.OrderItems.FindAll(oi => oi.Item.SellerId == id).Count != 0);
            return _mapper.Map<IEnumerable<DisplayOrderDTO>>(orders);
        }

        public Order ExtractOrderData(Order order)
        {
            return _dbContext.Orders.Include(o => o.Customer)
                                                 .Include(o => o.OrderItems)
                                                 .ThenInclude(o => o.Item)
                                                 .ThenInclude(p => p.Seller)
                                                 .Where(o => o.Id == order.Id)
                                                 .FirstOrDefault();
        }

    }
}
