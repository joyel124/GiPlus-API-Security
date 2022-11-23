using System.Net.Mime;
using AutoMapper;
using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Sales.Domain.Services;
using GiPlus.API.Sales.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Sales.Controllers;

[ApiController]
[Route("/api/v1/users/{userId}/sales")]
[Produces(MediaTypeNames.Application.Json)]
public class UserSalesController
{
    private readonly ISaleService _saleService;
    private readonly IMapper _mapper;

    public UserSalesController(ISaleService saleService, IMapper mapper)
    {
        _saleService = saleService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Sales for given User",
        Description = "Get existing sales associated with the specified User",
        OperationId = "GetUserSales",
        Tags = new []{"Users"}
    )]
    public async Task<IEnumerable<SaleResource>> GetAllByUserIdAsync(int userId)
    {
        var sales = await _saleService.ListByUserIdAsync(userId);

        var resources = _mapper.Map<IEnumerable<Sale>, IEnumerable<SaleResource>>(sales);

        return resources;
    }
}