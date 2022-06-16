using AutoMapper;
using BLL.DTO;
using DLL.Entities;

namespace BLL.MapperConfigurations
{
    public class CardConfigurationProfile : Profile
    {
        public CardConfigurationProfile()
        {
            CreateMap<Card, CardDTO>().ReverseMap();
        }
    }
}
