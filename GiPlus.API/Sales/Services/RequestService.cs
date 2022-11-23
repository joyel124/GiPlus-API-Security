using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Sales.Domain.Repositories;
using GiPlus.API.Sales.Domain.Services;
using GiPlus.API.Sales.Domain.Services.Communication;
using GiPlus.API.Security.Domain.Repositories;
using GiPlus.API.Shared.Domain.Repositories;

namespace GiPlus.API.Sales.Services;

public class RequestService : IRequestService
{
    private readonly IRequestRepository _requestRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public RequestService(IRequestRepository requestRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _requestRepository = requestRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }
    public async Task<IEnumerable<Request>> ListAsync()
    {
        return await _requestRepository.ListAsync();
    }

    public async Task<IEnumerable<Request>> ListByUserIdAsync(int userId)
    {
        return await _requestRepository.FindByUserIdAsync(userId);
    }

    public async Task<RequestResponse> SaveAsync(Request request)
    {
        //Validate UserId
        var existingUser = await _userRepository.FindByIdAsync(request.UserId);
        if (existingUser == null)
            return new RequestResponse("Invalid User");
        try
        {
            //Add request
            await _requestRepository.AddSync(request);
            //Complete transaction
            await _unitOfWork.CompleteAsync();
            //Return Response
            return new RequestResponse(request);
        }
        catch (Exception e)
        {
            //Error Handling
            return new RequestResponse($"An error occurred while saving the request: {e:Message}");
        }
    }

    public async Task<RequestResponse> UpdateAsync(int requestId, Request request)
    {
        var existingRequest = await _requestRepository.FindByIdAsync(requestId);
        //Validate client
        if (existingRequest == null)
            return new RequestResponse("Request not found");
        //Validate user
        var existingUser = await _userRepository.FindByIdAsync(request.UserId);
        if (existingUser == null)
            return new RequestResponse("Invalid User");
        
        //Modify Fields
        existingRequest.Name = request.Name;
        existingRequest.Brand = request.Brand;
        existingRequest.Date = request.Date;
        existingRequest.Status = request.Status;
        existingRequest.Description = request.Description;
        existingRequest.ClientId = request.ClientId;

        try
        {
            _requestRepository.Update(existingRequest);
            await _unitOfWork.CompleteAsync();

            return new RequestResponse(existingRequest);
        }
        catch (Exception e)
        {
            //Error Handling
            return new RequestResponse($"An error occurred while updating the request: {e.Message}");
        }
    }

    public async Task<RequestResponse> DeleteAsync(int requestId)
    {
        var existingRequest = await _requestRepository.FindByIdAsync(requestId);

        if (existingRequest == null)
            return new RequestResponse("Request not found");

        try
        {
            _requestRepository.Remove(existingRequest);
            await _unitOfWork.CompleteAsync();

            return new RequestResponse(existingRequest);
        }
        catch (Exception e)
        {
            return new RequestResponse($"An error occurred while deleting the request: {e.Message}");
        }
    }
}