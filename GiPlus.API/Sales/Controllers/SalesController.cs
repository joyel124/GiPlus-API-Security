using System.Net.Mime;
using AutoMapper;
using GiPlus.API.Management.Resources;
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
[SwaggerTag("Create, read, update and delete sales")]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;
    private readonly IMapper _mapper;

    public SalesController(ISaleService saleService, IMapper mapper){
        _saleService=saleService;
        _mapper=mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SaleResource>),statusCode:200)]
    public async Task<IEnumerable<SaleResource>> GetAllAsync(){
        var sale=await _saleService.ListAsync();
        var resources= _mapper.Map<IEnumerable<Sale>, IEnumerable<SaleResource>>(sale);

        return resources;
    }

    [HttpPost]
    [ProducesResponseType(typeof(SaleResource),201)]
    [ProducesResponseType(typeof(List<string>),400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The sale was successfully created", typeof(SaleResource))]
    [SwaggerResponse(400, "The sale data is not valid")]
    public async Task<IActionResult> PostAsync([FromBody] SaveSaleResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var sale = _mapper.Map<SaveSaleResource, Sale>(resource);

        var result = await _saleService.SaveAsync(sale);

        if (!result.Success)
            return BadRequest(result.Message);

        var saleResource = _mapper.Map<Sale, SaleResource>(result.Resource);

        return Ok(saleResource);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(SaleResource),201)]
    [ProducesResponseType(typeof(List<string>),400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The sale was successfully updated", typeof(SaleResource))]
    [SwaggerResponse(400, "The sale data is not valid")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveSaleResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var sale = _mapper.Map<SaveSaleResource, Sale>(resource);

        var result = await _saleService.UpdateAsync(id, sale);

        if (!result.Success)
            return BadRequest(result.Message);

        var saleResource = _mapper.Map<Sale, SaleResource>(result.Resource);

        return Ok(saleResource);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ClientResource),201)]
    [ProducesResponseType(typeof(List<string>),400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The sale was successfully deleted", typeof(ClientResource))]
    [SwaggerResponse(400, "The sale data is not valid")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _saleService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var saleResource = _mapper.Map<Sale, SaleResource>(result.Resource);

        return Ok(saleResource);
    }
}