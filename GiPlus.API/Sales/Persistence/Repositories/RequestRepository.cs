using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Sales.Domain.Repositories;
using GiPlus.API.Shared.Persistence.Contexts;
using GiPlus.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GiPlus.API.Sales.Persistence.Repositories;

public class RequestRepository : BaseRepository, IRequestRepository
{
    public RequestRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Request>> ListAsync()
    {
        return await _context.Requests
            .Include(p => p.User)
            .ToListAsync();
    }

    public async Task AddSync(Request request)
    {
        await _context.Requests.AddAsync(request);
    }

    public async Task<Request> FindByIdAsync(int requestId)
    {
        return await _context.Requests
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == requestId);
    }

    public async Task<Request> FindByNameAsync(string name)
    {
        return await _context.Requests
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<IEnumerable<Request>> FindByUserIdAsync(int userId)
    {
        return await _context.Requests
            .Where(p => p.UserId == userId)
            .Include(p => p.User)
            .ToListAsync();
    }

    public void Update(Request request)
    {
        _context.Requests.Update(request);
    }

    public void Remove(Request request)
    {
        _context.Requests.Remove(request);
    }
}