using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Shared.Domain.Services.Communication;

namespace GiPlus.API.Management.Domain.Services.Communication;

public class ClientResponse : BaseResponse<Client>
{
    public ClientResponse(string message) : base(message)
    {
    }

    public ClientResponse(Client resource) : base(resource)
    {
    }
}