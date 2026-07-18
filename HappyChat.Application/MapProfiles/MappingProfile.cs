using AutoMapper;
using HappyChat.Application.Features.UserFeatures.DTO;
using HappyChat.Core.Models;

namespace TaskManager.Business.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Auth Mappers
        CreateMap<RegisterRequest, User>().ReverseMap();
        #endregion
    }
}