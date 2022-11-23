using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Shared.Domain.Services.Communication;

namespace GiPlus.API.Sales.Domain.Services.Communication;

public class SaleResponse : BaseResponse<Sale>
{
    public SaleResponse(string message) : base(message)
    {
    }

    public SaleResponse(Sale resource) : base(resource)
    {
    }
}