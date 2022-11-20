using AutoMapper;
using GiPlus.API.Security.Domain.Models;
using GiPlus.API.Security.Domain.Services.Communication;
using GiPlus.API.Security.Resources;

namespace GiPlus.API.Security.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<User, AuthenticateResponse>();
        CreateMap<User, UserResource>();
    }
}