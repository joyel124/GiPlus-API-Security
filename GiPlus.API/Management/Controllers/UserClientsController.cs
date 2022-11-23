using System.Net.Mime;
using AutoMapper;
using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Management.Domain.Services;
using GiPlus.API.Management.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Management.Controllers;

[ApiController]
[Route("/api/v1/users/{userId}/clients")]
[Produces(MediaTypeNames.Application.Json)]
public class UserClientsController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly IMapper _mapper;

    public UserClientsController(IClientService clientService, IMapper mapper)
    {
        _clientService = clientService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Clients for given User",
        Description = "Get existing clients associated with the specified User",
        OperationId = "GetUserClients",
        Tags = new []{"Users"}
    )]
    public async Task<IEnumerable<ClientResource>> GetAllByUserIdAsync(int userId)
    {
        var clients = await _clientService.ListByUserIdAsync(userId);

        var resources = _mapper.Map<IEnumerable<Client>, IEnumerable<ClientResource>>(clients);

        return resources;
    }
}