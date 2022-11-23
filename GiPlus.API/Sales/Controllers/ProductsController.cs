using System.Net.Mime;
using AutoMapper;
using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Sales.Domain.Services;
using GiPlus.API.Sales.Resources;
using GiPlus.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Sales.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Create, read, update and delete products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductsController(IProductService productService, IMapper mapper){
        _productService=productService;
        _mapper=mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductResource>),statusCode:200)]
    public async Task<IEnumerable<ProductResource>> GetAllAsync(){
        var product=await _productService.ListAsync();
        var resources= _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(product);

        return resources;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductResource),201)]
    [ProducesResponseType(typeof(List<string>),400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The product was successfully created", typeof(ProductResource))]
    [SwaggerResponse(400, "The product data is not valid")]
    public async Task<IActionResult> PostAsync([FromBody] SaveProductResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var client = _mapper.Map<SaveProductResource, Product>(resource);

        var result = await _productService.SaveAsync(client);

        if (!result.Success)
            return BadRequest(result.Message);

        var productResource = _mapper.Map<Product, ProductResource>(result.Resource);

        return Ok(productResource);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ProductResource),201)]
    [ProducesResponseType(typeof(List<string>),400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The product was successfully updated", typeof(ProductResource))]
    [SwaggerResponse(400, "The product data is not valid")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveProductResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var client = _mapper.Map<SaveProductResource, Product>(resource);

        var result = await _productService.UpdateAsync(id, client);

        if (!result.Success)
            return BadRequest(result.Message);

        var productResource = _mapper.Map<Product, ProductResource>(result.Resource);

        return Ok(productResource);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ProductResource),201)]
    [ProducesResponseType(typeof(List<string>),400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The product was successfully deleted", typeof(ProductResource))]
    [SwaggerResponse(400, "The product data is not valid")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _productService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var productResource = _mapper.Map<Product, ProductResource>(result.Resource);

        return Ok(productResource);
    }
}