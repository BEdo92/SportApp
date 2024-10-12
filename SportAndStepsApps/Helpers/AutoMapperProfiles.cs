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
    }
}
