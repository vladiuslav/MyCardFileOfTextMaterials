using AutoMapper;
using BLL.DTO;
using WEBAPI.Models;

namespace WEBAPI
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserInfoModel, UserDTO>()
                .ForMember(dto => dto.Cards, model => model.Ignore())
                .ForMember(dto => dto.Id, model => model.Ignore())
                .ForMember(dto => dto.Role, model => model.Ignore())
                .ReverseMap();

            CreateMap<UserRegistrationModel, UserDTO>()
                .ForMember(dto => dto.Cards, model => model.Ignore())
                .ForMember(dto => dto.Id, model => model.Ignore())
                .ForMember(dto => dto.Role, model => model.Ignore())
                .ReverseMap();
            CreateMap<UserDTO, UserShortInfoModel>();
            CreateMap<UserDTO, UserUpdateModel>().ReverseMap();

            CreateMap<CardDTO, CardCreationModel>().ReverseMap();
            CreateMap<CardDTO, CardUpdateModel>().ReverseMap();
            CreateMap<CardDTO, CardInfoModel>()
                .ForMember(dto => dto.CategoryName, model => model.Ignore())
                .ForMember(dto => dto.UserName, model => model.Ignore())
                .ReverseMap();

            CreateMap<CategoryDTO, CategoryCreationModel>().ReverseMap();
            CreateMap<CategoryDTO, CategoryInfoModel>().ReverseMap();
        }
    }
}
