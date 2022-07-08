using AutoMapper;
using BLL.DTO;
using WEBAPI.Models;
using System.Linq;

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
                .ForMember(dto => dto.Likes, model => model.MapFrom(card => card.Likes.Where(like=>like.IsDislike==false).ToList().Count))
                .ForMember(dto => dto.DisLikes, model => model.MapFrom(card => card.Likes.Where(like => like.IsDislike == true).ToList().Count))
                .ReverseMap();

            CreateMap<CategoryDTO, CategoryCreationModel>().ReverseMap();
            CreateMap<CategoryDTO, CategoryInfoModel>().ReverseMap();
        }
    }
}
