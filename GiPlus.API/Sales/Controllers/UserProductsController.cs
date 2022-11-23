using System.Net.Mime;
using AutoMapper;
using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Sales.Domain.Services;
using GiPlus.API.Sales.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Sales.Controllers;

[ApiController]
[Route("/api/v1/users/{userId}/products")]
[Produces(MediaTypeNames.Application.Json)]
public class UserProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public UserProductController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Products for given User",
        Description = "Get existing products associated with the specified User",
        OperationId = "GetUserProducts",
        Tags = new []{"Users"}
    )]
    public async Task<IEnumerable<ProductResource>> GetAllByUserIdAsync(int userId)
    {
        var products = await _productService.ListByUserIdAsync(userId);

        var resources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);

        return resources;
    }
}