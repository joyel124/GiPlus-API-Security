using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Sales.Domain.Services.Communication;

namespace GiPlus.API.Sales.Domain.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> ListAsync();
    Task<IEnumerable<Product>> ListByUserIdAsync(int userId);
    Task<ProductResponse> SaveAsync(Product product);
    Task<ProductResponse> UpdateAsync(int productId, Product product);
    Task<ProductResponse> DeleteAsync(int productId);
}