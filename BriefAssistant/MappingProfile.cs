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
            CreateMap<BriefInfo, BriefDto>(MemberList.None).ReverseMap();
            CreateMap<ContactInfo, ContactInfoDto>(MemberList.None)
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address.State))
                .ForMember(dest => dest.Zip, opt => opt.MapFrom(src => src.Address.Zip))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.Street2, opt => opt.MapFrom(src => src.Address.Street2))
                .ReverseMap();
            CreateMap<CircuitCourtCase, CaseDto>(MemberList.None).ReverseMap();

            CreateMap<BriefDto, BriefListItem>();
        }
    }
}
