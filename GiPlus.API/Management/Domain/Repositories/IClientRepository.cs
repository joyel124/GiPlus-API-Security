using GiPlus.API.Management.Domain.Models;

namespace GiPlus.API.Management.Domain.Repositories;

public interface IClientRepository
{
    Task<IEnumerable<Client>> ListAsync();
    Task AddSync(Client client);
    Task<Client> FindByIdAsync(int clientId);
    Task<Client> FindByNameAsync(string name);
    Task<IEnumerable<Client>> FindByUserIdAsync(int userId);
    void Update(Client client);
    void Remove(Client client);
}