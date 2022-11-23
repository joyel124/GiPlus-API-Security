using GiPlus.API.Sales.Domain.Models;

namespace GiPlus.API.Sales.Domain.Repositories;

public interface IRequestRepository
{
    Task<IEnumerable<Request>> ListAsync();
    Task AddSync(Request request);
    Task<Request> FindByIdAsync(int requestId);
    Task<Request> FindByNameAsync(string name);
    Task<IEnumerable<Request>> FindByUserIdAsync(int userId);
    void Update(Request request);
    void Remove(Request request);
}