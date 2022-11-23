using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Management.Domain.Repositories;
using GiPlus.API.Management.Domain.Services;
using GiPlus.API.Management.Domain.Services.Communication;
using GiPlus.API.Security.Domain.Repositories;
using GiPlus.API.Shared.Domain.Repositories;

namespace GiPlus.API.Management.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public ClientService(IClientRepository clientRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }
    public async Task<IEnumerable<Client>> ListAsync()
    {
        return await _clientRepository.ListAsync();
    }

    public async Task<IEnumerable<Client>> ListByUserIdAsync(int userId)
    {
        return await _clientRepository.FindByUserIdAsync(userId);
    }

    public async Task<ClientResponse> SaveAsync(Client client)
    {
        //Validate UserId
        var existingUser = await _userRepository.FindByIdAsync(client.UserId);
        if (existingUser == null)
            return new ClientResponse("Invalid User");
        //Validate client Name
        try
        {
            //Add client
            await _clientRepository.AddSync(client);
            //Complete transaction
            await _unitOfWork.CompleteAsync();
            //Return Response
            return new ClientResponse(client);
        }
        catch (Exception e)
        {
            //Error Handling
            return new ClientResponse($"An error occurred while saving the client: {e:Message}");
        }
    }

    public async Task<ClientResponse> UpdateAsync(int clientId, Client client)
    {
        var existingClient = await _clientRepository.FindByIdAsync(clientId);
        //Validate client
        if (existingClient == null)
            return new ClientResponse("Client not found");
        //Validate user
        var existingUser = await _userRepository.FindByIdAsync(client.UserId);
        if (existingUser == null)
            return new ClientResponse("Invalid User");
        
        //Modify Fields
        existingClient.DocumentType = client.DocumentType;
        existingClient.NumberIdentification = client.NumberIdentification;
        existingClient.FirstName = client.FirstName;
        existingClient.LastName = client.LastName;
        existingClient.Phone = client.Phone;
        existingClient.Email = client.Email;
        existingClient.City = client.City;
        existingClient.Address=client.Address;

        try
        {
            _clientRepository.Update(existingClient);
            await _unitOfWork.CompleteAsync();

            return new ClientResponse(existingClient);
        }
        catch (Exception e)
        {
            //Error Handling
            return new ClientResponse($"An error occurred while updating the client: {e.Message}");
        }
    }

    public async Task<ClientResponse> DeleteAsync(int clientId)
    {
        var existingClient = await _clientRepository.FindByIdAsync(clientId);

        if (existingClient == null)
            return new ClientResponse("Client not found");

        try
        {
            _clientRepository.Remove(existingClient);
            await _unitOfWork.CompleteAsync();

            return new ClientResponse(existingClient);
        }
        catch (Exception e)
        {
            return new ClientResponse($"An error occurred while deleting the client: {e.Message}");
        }
    }
}