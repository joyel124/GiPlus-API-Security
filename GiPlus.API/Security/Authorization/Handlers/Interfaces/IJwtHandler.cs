using GiPlus.API.Security.Domain.Models;

namespace GiPlus.API.Security.Authorization.Handlers.Interfaces;

public interface IJwtHandler
{
    string GenerateToken(User user);
    int? ValidateToken(string token);
}