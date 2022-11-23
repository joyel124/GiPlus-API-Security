using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Shared.Domain.Services.Communication;

namespace GiPlus.API.Management.Domain.Services.Communication;

public class ClientResponse : BaseResponse<Client>
{
    protected ClientResponse(string message) : base(message)
    {
    }

    protected ClientResponse(Client resource) : base(resource)
    {
    }
}