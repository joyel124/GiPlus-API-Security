using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Shared.Domain.Services.Communication;

namespace GiPlus.API.Sales.Domain.Services.Communication;

public class SaleResponse : BaseResponse<Sale>
{
    protected SaleResponse(string message) : base(message)
    {
    }

    protected SaleResponse(Sale resource) : base(resource)
    {
    }
}