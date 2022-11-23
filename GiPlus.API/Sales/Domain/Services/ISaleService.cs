using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Sales.Domain.Services.Communication;

namespace GiPlus.API.Sales.Domain.Services;

public interface ISaleService
{
    Task<IEnumerable<Sale>> ListAsync();
    Task<IEnumerable<Sale>> ListByUserIdAsync();
    Task<SaleResponse> SaveAsync(Sale sale);
    Task<SaleResponse> UpdateAsync(int saleId, Sale sale);
    Task<SaleResponse> DeleteAsync(int saleId);
}