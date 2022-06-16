using AutoMapper;
using BLL.DTO;
using DLL.Entities;

namespace BLL.MapperConfigurations
{
    public class UserConfigurationProfile : Profile
    {
        public UserConfigurationProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
