using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Sales.Domain.Services.Communication;

namespace GiPlus.API.Sales.Domain.Services;

public interface IRequestService
{
    Task<IEnumerable<Request>> ListAsync();
    Task<IEnumerable<Request>> ListByUserIdAsync();
    Task<RequestResponse> SaveAsync(Request request);
    Task<RequestResponse> UpdateAsync(int requestId, Request request);
    Task<RequestResponse> DeleteAsync(int requestId);
}