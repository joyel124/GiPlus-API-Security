using AutoMapper;
using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Management.Resources;

namespace GiPlus.API.Management.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Client, ClientResource>();
        CreateMap<Employee, EmployeeResource>();
        CreateMap<Supplier, SupplierResource>();
    }
}