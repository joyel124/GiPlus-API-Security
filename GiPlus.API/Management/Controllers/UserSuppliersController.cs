using System.Net.Mime;
using AutoMapper;
using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Management.Domain.Services;
using GiPlus.API.Management.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Management.Controllers;

[ApiController]
[Route("/api/v1/users/{userId}/suppliers")]
[Produces(MediaTypeNames.Application.Json)]
public class UserSuppliersController : ControllerBase
{
    private readonly ISupplierService _supplierService;
    private readonly IMapper _mapper;

    public UserSuppliersController(ISupplierService supplierService, IMapper mapper)
    {
        _supplierService = supplierService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Suppliers for given User",
        Description = "Get existing suppliers associated with the specified User",
        OperationId = "GetUserSuppliers",
        Tags = new []{"Users"}
    )]
    public async Task<IEnumerable<SupplierResource>> GetAllByUserIdAsync(int userId)
    {
        var suppliers = await _supplierService.ListByUserIdAsync(userId);

        var resources = _mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierResource>>(suppliers);

        return resources;
    }
}