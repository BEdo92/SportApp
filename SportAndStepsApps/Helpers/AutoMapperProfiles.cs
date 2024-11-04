using AutoMapper;
using SportAndStepsApps.DTOs;
using SportAndStepsApps.Models;

namespace SportAndStepsApps.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<User, MemberDto>();
        CreateMap<MemberDto, User>();
        CreateMap<RegisterDto, User>();
        CreateMap<SportDto, UserActivity>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.Distance, opt => opt.MapFrom(src => src.Distance));
        CreateMap<IGrouping<SportType, UserActivity>, SportSummaryDto>()
            .ForMember(dest => dest.SportType, opt => opt.MapFrom(src => src.Key.Name))  // The key of IGrouping is SportType
            .ForMember(dest => dest.Distance, opt => opt.MapFrom(src => src.Sum(u => u.Distance)));  // Summing the distance
        CreateMap<UserActivity, SportDto>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.Distance, opt => opt.MapFrom(src => src.Distance))
            .ForMember(dest => dest.SportType, opt => opt.MapFrom(src => src.SportType.Name));
    }
}
