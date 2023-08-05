using AutoMapper;
using WebApplication.DTO;
using WebApplication.Models;

namespace WebApplication.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, RegisterDTO>().ReverseMap();
        }
    }
}
