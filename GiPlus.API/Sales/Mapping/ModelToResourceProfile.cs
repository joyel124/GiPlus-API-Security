using AutoMapper;
using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Sales.Resources;

namespace GiPlus.API.Sales.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Product, ProductResource>();
        CreateMap<Sale, SaleResource>();
        CreateMap<Request, RequestResource>();
    }
}