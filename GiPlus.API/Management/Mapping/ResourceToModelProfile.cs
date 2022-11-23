using AutoMapper;
using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Management.Resources;

namespace GiPlus.API.Management.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveClientResource, Client>();
        CreateMap<SaveEmployeeResource, Employee>();
        CreateMap<SaveSupplierResource, Supplier>();
    }
}