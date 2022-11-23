using AutoMapper;
using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Sales.Resources;

namespace GiPlus.API.Sales.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveProductResource, Product>();
        CreateMap<SaveSaleResource, Sale>();
        CreateMap<SaveRequestResource, Request>();
    }
}