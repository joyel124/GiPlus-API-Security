using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Management.Domain.Services.Communication;

namespace GiPlus.API.Management.Domain.Services;

public interface IClientService
{
    Task<IEnumerable<Client>> ListAsync();
    Task<IEnumerable<Client>> ListByUserIdAsync();
    Task<ClientResponse> SaveAsync(Client client);
    Task<ClientResponse> UpdateAsync(int clientId, Client client);
    Task<ClientResponse> DeleteAsync(int clientId);
}