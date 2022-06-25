﻿using AutoMapper;
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
            CreateMap<UserInfoModel, UserDTO>()
                .ForMember(dto => dto.Cards, model => model.Ignore())
                .ReverseMap();

            CreateMap<UserRegistrationModel, UserDTO>()
                .ForMember(dto => dto.Cards, model => model.Ignore())
                .ForMember(dto => dto.Id, model => model.Ignore())
                .ForMember(dto => dto.Role, model => model.Ignore())
                .ReverseMap();
            CreateMap<UserDTO, UserShortInfoModel>();
            CreateMap<UserDTO, UserUpdateModel>().ReverseMap();

            CreateMap<CardDTO, CardCreationModel>().ReverseMap();
            CreateMap<CardDTO, CardInfoModel>().ReverseMap();

            CreateMap<CategoryDTO, CategoryCreationModel>().ReverseMap(); 
            CreateMap<CategoryDTO, CategoryInfoModel>().ReverseMap();
        }
    }
}