using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Shared.Domain.Services.Communication;

namespace GiPlus.API.Sales.Domain.Services.Communication;

public class ProductResponse : BaseResponse<Product>
{
    public ProductResponse(string message) : base(message)
    {
    }

    public ProductResponse(Product resource) : base(resource)
    {
    }
}