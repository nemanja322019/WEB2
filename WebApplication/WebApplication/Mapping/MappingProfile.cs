using AutoMapper;
using WebApplication.DTO.ItemDTO;
using WebApplication.DTO.UserDTO;
using WebApplication.Models;

namespace WebApplication.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, RegisterDTO>().ReverseMap();
            CreateMap<User, DisplayProfileDTO>().ReverseMap();


            CreateMap<Item, DisplayItemDTO>().ReverseMap();
            CreateMap<Item, CreateItemDTO>().ReverseMap();

        }
    }
}
