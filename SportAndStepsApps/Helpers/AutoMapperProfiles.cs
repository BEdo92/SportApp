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
    }
}
