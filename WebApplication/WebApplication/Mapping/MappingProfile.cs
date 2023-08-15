using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication.DTO.ItemDTO;
using WebApplication.DTO.OrderDTO;
using WebApplication.DTO.UserDTO;
using WebApplication.Enums;
using WebApplication.Models;

namespace WebApplication.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //USERS
            CreateMap<User, RegisterDTO>().ReverseMap();
            CreateMap<User, DisplayProfileDTO>().ReverseMap();

            //ITEMS
            CreateMap<Item, DisplayItemDTO>().ReverseMap();
            CreateMap<Item, CreateItemDTO>().ReverseMap();

            //ORDERS
            CreateMap<Order, DisplayOrderDTO>().ForMember(dest => dest.Status, opt => opt.MapFrom(src => GetOrderStatus(src))).ReverseMap();
            CreateMap<Order, NewOrderDTO>().ReverseMap();
            CreateMap<NewOrderDTO, Order>().ForMember(dest => dest.OrderItems, opt => opt.Ignore());

            CreateMap<OrderItem, DisplayItemDTO>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Item.Id))
                                                        .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Item.ItemName))
                                                        .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Item.Description))
                                                        .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Item.Price))
                                                        .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount));

        }

        private string GetOrderStatus(Order order)
        {
            if(order.IsCanceled)
                return OrderStatus.CANCELED.ToString();
            else if(order.DeliveryTime > DateTime.Now)
                return OrderStatus.ONGOING.ToString();
            else 
                return OrderStatus.DELIVERED.ToString();
        }
    }
}
