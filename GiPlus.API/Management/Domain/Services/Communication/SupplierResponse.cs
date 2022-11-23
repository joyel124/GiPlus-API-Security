using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Shared.Domain.Services.Communication;

namespace GiPlus.API.Management.Domain.Services.Communication;

public class SupplierResponse : BaseResponse<Supplier>
{
    public SupplierResponse(string message) : base(message)
    {
    }

    public SupplierResponse(Supplier resource) : base(resource)
    {
    }
}