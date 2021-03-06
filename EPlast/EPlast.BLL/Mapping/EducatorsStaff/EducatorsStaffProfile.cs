﻿using AutoMapper;
using EPlast.BLL.DTO.EducatorsStaff;
using EPlast.DataAccess.Entities.EducatorsStaff;

namespace EPlast.BLL.Mapping.EducationsStaff
{
    public class EducatorsStaffProfile:Profile
    {
        public EducatorsStaffProfile()
        {
            CreateMap<EducatorsStaff, EducatorsStaffDTO>().ReverseMap();
        }
        
    }
}
