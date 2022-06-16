using AutoMapper;
using BLL.DTO;
using DLL.Entities;

namespace BLL.MapperConfigurations
{
    public class CategoryConfigurationProfile : Profile
    {
        public CategoryConfigurationProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
