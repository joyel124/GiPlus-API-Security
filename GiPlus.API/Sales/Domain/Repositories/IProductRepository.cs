using GiPlus.API.Sales.Domain.Models;

namespace GiPlus.API.Sales.Domain.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> ListAsync();
    Task AddSync(Product product);
    Task<Product> FindByIdAsync(int productId);
    Task<Product> FindByNameAsync(string name);
    Task<IEnumerable<Product>> FindByUserIdAsync(int userId);
    void Update(Product product);
    void Remove(Product product);
}