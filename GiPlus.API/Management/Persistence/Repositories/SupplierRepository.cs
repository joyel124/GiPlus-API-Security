using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Management.Domain.Repositories;
using GiPlus.API.Shared.Persistence.Contexts;
using GiPlus.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GiPlus.API.Management.Persistence.Repositories;

public class SupplierRepository : BaseRepository, ISupplierRepository
{
    public SupplierRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Supplier>> ListAsync()
    {
        return await _context.Suppliers
            .Include(p => p.User)
            .ToListAsync();
    }

    public async Task AddSync(Supplier supplier)
    {
        await _context.Suppliers.AddAsync(supplier);
    }

    public async Task<Supplier> FindByIdAsync(int supplierId)
    {
        return await _context.Suppliers
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == supplierId);
    }

    public async Task<Supplier> FindByNameAsync(string name)
    {
        return await _context.Suppliers
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<IEnumerable<Supplier>> FindByUserIdAsync(int userId)
    {
        return await _context.Suppliers
            .Where(p => p.UserId == userId)
            .Include(p => p.User)
            .ToListAsync();
    }

    public void Update(Supplier supplier)
    {
        _context.Suppliers.Update(supplier);
    }

    public void Remove(Supplier supplier)
    {
        _context.Suppliers.Remove(supplier);
    }
}