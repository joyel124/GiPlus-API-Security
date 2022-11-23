using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Management.Domain.Repositories;
using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Shared.Persistence.Contexts;
using GiPlus.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GiPlus.API.Management.Persistence.Repositories;

public class ClientRepository : BaseRepository, IClientRepository
{
    public ClientRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Client>> ListAsync()
    {
        return await _context.Clients
            .Include(p => p.User)
            .ToListAsync();
    }

    public async Task AddSync(Client client)
    {
        await _context.Clients.AddAsync(client);
    }

    public async Task<Client> FindByIdAsync(int clientId)
    {
        return await _context.Clients
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == clientId);
    }

    public async Task<Client> FindByNameAsync(string name)
    {
        return await _context.Clients
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.FirstName == name);
    }

    public async Task<IEnumerable<Client>> FindByUserIdAsync(int userId)
    {
        return await _context.Clients
            .Where(p => p.UserId == userId)
            .Include(p => p.User)
            .ToListAsync();
    }

    public void Update(Client client)
    {
        _context.Clients.Update(client);
    }

    public void Remove(Client client)
    {
        _context.Clients.Remove(client);
    }
}