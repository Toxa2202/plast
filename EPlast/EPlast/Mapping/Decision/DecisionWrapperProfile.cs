﻿using AutoMapper;
using EPlast.BLL.DTO;
using EPlast.Models;

namespace EPlast.Mapping
{
    public class DecisionWrapperProfile : Profile
    {
        public DecisionWrapperProfile()
        {
            CreateMap<DecisionWrapper, DecisionWrapperDTO>()
                .ForMember(d => d.Decision, o => o.MapFrom(s => s.Decision))
                .ReverseMap();
        }
    }
}