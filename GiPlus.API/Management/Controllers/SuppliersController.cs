using System.Net.Mime;
using AutoMapper;
using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Management.Domain.Services;
using GiPlus.API.Management.Resources;
using GiPlus.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Management.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Create, read, update and delete suppliers")]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _supplierService;
    private readonly IMapper _mapper;

    public SuppliersController(ISupplierService supplierService, IMapper mapper){
        _supplierService=supplierService;
        _mapper=mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SupplierResource>),statusCode:200)]
    public async Task<IEnumerable<SupplierResource>> GetAllAsync(){
        var supplier=await _supplierService.ListAsync();
        var resources= _mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierResource>>(supplier);

        return resources;
    }

    [HttpPost]
    [ProducesResponseType(typeof(SupplierResource),201)]
    [ProducesResponseType(typeof(List<string>),400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The supplier was successfully created", typeof(SupplierResource))]
    [SwaggerResponse(400, "The supplier data is not valid")]
    public async Task<IActionResult> PostAsync([FromBody] SaveSupplierResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var supplier = _mapper.Map<SaveSupplierResource, Supplier>(resource);

        var result = await _supplierService.SaveAsync(supplier);

        if (!result.Success)
            return BadRequest(result.Message);

        var supplierResource = _mapper.Map<Supplier, SupplierResource>(result.Resource);

        return Ok(supplierResource);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(SupplierResource),201)]
    [ProducesResponseType(typeof(List<string>),400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The supplier was successfully updated", typeof(SupplierResource))]
    [SwaggerResponse(400, "The supplier data is not valid")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveSupplierResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var supplier = _mapper.Map<SaveSupplierResource, Supplier>(resource);

        var result = await _supplierService.UpdateAsync(id, supplier);

        if (!result.Success)
            return BadRequest(result.Message);

        var supplierResource = _mapper.Map<Supplier, SupplierResource>(result.Resource);

        return Ok(supplierResource);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(SupplierResource),201)]
    [ProducesResponseType(typeof(List<string>),400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The supplier was successfully deleted", typeof(SupplierResource))]
    [SwaggerResponse(400, "The supplier data is not valid")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _supplierService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var supplierResource = _mapper.Map<Supplier, SupplierResource>(result.Resource);

        return Ok(supplierResource);
    }
}