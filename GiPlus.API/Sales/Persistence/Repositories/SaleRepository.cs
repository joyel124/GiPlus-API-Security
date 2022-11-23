using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Sales.Domain.Repositories;
using GiPlus.API.Shared.Persistence.Contexts;
using GiPlus.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GiPlus.API.Sales.Persistence.Repositories;

public class SaleRepository : BaseRepository, ISaleRepository
{
    public SaleRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Sale>> ListAsync()
    {
        return await _context.Sales
            .Include(p => p.User)
            .ToListAsync();
    }

    public async Task AddSync(Sale sale)
    {
        await _context.Sales.AddAsync(sale);
    }

    public async Task<Sale> FindByIdAsync(int saleId)
    {
        return await _context.Sales
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == saleId);
    }

    public async Task<Sale> FindByNameAsync(string name)
    {
        return await _context.Sales
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Date == name);
    }

    public async Task<IEnumerable<Sale>> FindByUserIdAsync(int userId)
    {
        return await _context.Sales
            .Where(p => p.UserId == userId)
            .Include(p => p.User)
            .ToListAsync();
    }

    public void Update(Sale sale)
    {
        _context.Sales.Update(sale);
    }

    public void Remove(Sale sale)
    {
        _context.Sales.Remove(sale);
    }
}