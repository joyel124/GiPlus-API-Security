using GiPlus.API.Management.Domain.Models;

namespace GiPlus.API.Management.Domain.Repositories;

public interface ISupplierRepository
{
    Task<IEnumerable<Supplier>> ListAsync();
    Task AddSync(Supplier supplier);
    Task<Supplier> FindByIdAsync(int supplierId);
    Task<Supplier> FindByNameAsync(string name);
    Task<IEnumerable<Supplier>> FindByUserIdAsync(int userId);
    void Update(Supplier supplier);
    void Remove(Supplier supplier);
}