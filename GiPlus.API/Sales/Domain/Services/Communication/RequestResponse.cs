using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Shared.Domain.Services.Communication;

namespace GiPlus.API.Sales.Domain.Services.Communication;

public class RequestResponse : BaseResponse<Request>
{
    public RequestResponse(string message) : base(message)
    {
    }

    public RequestResponse(Request resource) : base(resource)
    {
    }
}