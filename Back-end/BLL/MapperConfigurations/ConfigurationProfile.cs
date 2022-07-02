using AutoMapper;
using BLL.DTO;
using DLL.Entities;

namespace BLL.MapperConfigurations
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Card, CardDTO>().ReverseMap();
            CreateMap<Like, LikeDTO>().ReverseMap();
        }
    }
}
