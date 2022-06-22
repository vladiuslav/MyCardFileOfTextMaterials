using AutoMapper;
using BLL.DTO;
using WEBAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserAllInfoModel, UserDTO>()
                .ForMember(dto => dto.Cards, model => model.Ignore())
                .ReverseMap();

            CreateMap<UserRegistrationModel, UserDTO>()
                .ForMember(dto => dto.Cards, model => model.Ignore())
                .ForMember(dto => dto.Id, model => model.Ignore())
                .ReverseMap();
            CreateMap<UserDTO, UserShortInfoModel>();
        }
    }
}
