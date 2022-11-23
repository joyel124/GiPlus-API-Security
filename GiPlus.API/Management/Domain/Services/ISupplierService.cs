using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Management.Domain.Services.Communication;

namespace GiPlus.API.Management.Domain.Services;

public interface ISupplierService
{
    Task<IEnumerable<Supplier>> ListAsync();
    Task<IEnumerable<Supplier>> ListByUserIdAsync();
    Task<SupplierResponse> SaveAsync(Supplier supplier);
    Task<SupplierResponse> UpdateAsync(int supplierId, Supplier supplier);
    Task<SupplierResponse> DeleteAsync(int supplierId);
}