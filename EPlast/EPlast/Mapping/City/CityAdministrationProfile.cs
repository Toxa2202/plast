﻿using AutoMapper;
using EPlast.BLL.DTO.City;
using EPlast.DataAccess.Entities;
using EPlast.ViewModels.City;

namespace EPlast.Mapping.City
{
    public class CityAdministrationProfile : Profile
    {
        public CityAdministrationProfile()
        {
            CreateMap<CityAdministrationViewModel, CityAdministrationDTO>().ReverseMap();
        }
    }
}
