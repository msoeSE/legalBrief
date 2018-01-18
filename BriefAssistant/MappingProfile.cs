using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BriefAssistant.Data;
using BriefAssistant.Models;

namespace BriefAssistant
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BriefInfo, BriefDto>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoDto>().ReverseMap();
            CreateMap<CircuitCourtCase, CaseDto>().ReverseMap();

            CreateMap<BriefDto, BriefListItem>();
        }
    }
}
