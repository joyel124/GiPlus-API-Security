using GiPlus.API.Shared.Domain.Repositories;
using GiPlus.API.Shared.Persistence.Contexts;

namespace GiPlus.API.Shared.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context){
        _context=context;
    }
    public async Task CompleteAsync(){
        await _context.SaveChangesAsync();
    }
}