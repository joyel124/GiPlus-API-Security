using System.Net.Mime;
using AutoMapper;
using GiPlus.API.Management.Domain.Services;
using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Sales.Domain.Services;
using GiPlus.API.Sales.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Sales.Controllers;

[ApiController]
[Route("/api/v1/users/{userId}/requests")]
[Produces(MediaTypeNames.Application.Json)]
public class UserRequestsController : ControllerBase
{
    private readonly IRequestService _requestService;
    private readonly IMapper _mapper;

    public UserRequestsController(IRequestService requestService, IMapper mapper)
    {
        _requestService = requestService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Requests for given User",
        Description = "Get existing requests associated with the specified User",
        OperationId = "GetUserRequests",
        Tags = new []{"Users"}
    )]
    public async Task<IEnumerable<RequestResource>> GetAllByUserIdAsync(int userId)
    {
        var requests = await _requestService.ListByUserIdAsync(userId);

        var resources = _mapper.Map<IEnumerable<Request>, IEnumerable<RequestResource>>(requests);

        return resources;
    }
}