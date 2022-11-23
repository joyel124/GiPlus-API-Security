using AutoMapper;
using GiPlus.API.Security.Authorization.Attributes;
using GiPlus.API.Security.Domain.Models;
using GiPlus.API.Security.Domain.Services;
using GiPlus.API.Security.Domain.Services.Communication;
using GiPlus.API.Security.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Security.Controllers;

[Authorize]
[ApiController]
[Route("/api/v1/[controller]")]
[SwaggerTag("Create, read, update and delete Users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;


    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("sign-in")]
    [ProducesResponseType(typeof(UserResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The user was successfully created")]
    [SwaggerResponse(400, "The user data is not valid")]
    public async Task<IActionResult> AuthenticateAsync(AuthenticateRequest request)
    {
        var response = await _userService.Authenticate(request);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("sign-up")]
    [ProducesResponseType(typeof(UserResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The user was successfully created")]
    [SwaggerResponse(400, "The user data is not valid")]
    public async Task<IActionResult> RegisterAsync(RegisterRequest request)
    {
        await _userService.RegisterAsync(request);
        return Ok(new { message = "Registration successful" });
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserResource>), statusCode:200)]
    public async Task<IActionResult> GetAllAsync()//obtener todos los usuarios
    {
        var users = await _userService.ListAsync();
        var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)//obtener los usuarios por id
    {
        var user = await _userService.GetByIdAsync(id);
        var resource = _mapper.Map<User, UserResource>(user);
        return Ok(resource);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UserResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The user was successfully updated")]
    [SwaggerResponse(400, "The user data is not valid")]
    public async Task<IActionResult> UpdateAsync(int id, UpdateRequest request)//actualizar por id
    {
        await _userService.UpdateAsync(id, request);
        return Ok(new { message = "User updated successfully" });
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(UserResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The user was successfully deleted")]
    [SwaggerResponse(400, "The user data is not valid")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _userService.DeleteAsync(id);
        return Ok(new { message = "User deleted successfully" });
    }
}