using GiPlus.API.Sales.Domain.Models;

namespace GiPlus.API.Sales.Domain.Repositories;

public interface ISaleRepository
{
    Task<IEnumerable<Sale>> ListAsync();
    Task AddSync(Sale sale);
    Task<Sale> FindByIdAsync(int saleId);
    Task<Sale> FindByNameAsync(string name);
    Task<IEnumerable<Sale>> FindByUserIdAsync(int userId);
    void Update(Sale sale);
    void Remove(Sale sale);
}