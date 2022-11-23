using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Sales.Domain.Repositories;
using GiPlus.API.Shared.Persistence.Contexts;
using GiPlus.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GiPlus.API.Sales.Persistence.Repositories;

public class ProductRepository : BaseRepository, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> ListAsync()
    {
        return await _context.Products
            .Include(p => p.User)
            .ToListAsync();
    }

    public async Task AddSync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public async Task<Product> FindByIdAsync(int productId)
    {
        return await _context.Products
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task<Product> FindByNameAsync(string name)
    {
        return await _context.Products
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<IEnumerable<Product>> FindByUserIdAsync(int userId)
    {
        return await _context.Products
            .Where(p => p.UserId == userId)
            .Include(p => p.User)
            .ToListAsync();
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
    }

    public void Remove(Product product)
    {
        _context.Products.Remove(product);
    }
}