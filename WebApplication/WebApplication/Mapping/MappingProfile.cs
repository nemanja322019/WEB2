using AutoMapper;
using WebApplication.DTO;
using WebApplication.Models;

namespace WebApplication.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Customer, RegisterDTO>().ReverseMap();
            CreateMap<Seller, RegisterDTO>().ReverseMap();
            CreateMap<Admin, RegisterDTO>().ReverseMap();
        }
    }
}
