using GiPlus.API.Security.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Management.Resources;

public class ClientResource
{
    [SwaggerSchema("Client identifier")]
    public int Id { get; set; }
    [SwaggerSchema("Client Document Type")]
    public string DocumentType { get; set; }
    [SwaggerSchema("Client Number Identification")]
    public int NumberIdentification { get; set; }
    [SwaggerSchema("Client First Name")]
    public string FirstName { get; set; }
    [SwaggerSchema("Client Last Name")]
    public string LastName { get; set; }
    [SwaggerSchema("Client Phone")]
    public int Phone { get; set; }
    [SwaggerSchema("Client Email")]
    public string Email { get; set; }
    [SwaggerSchema("Client City")]
    public string City { get; set; }
    [SwaggerSchema("Client Address")]
    public string Address { get; set; }
    
    [SwaggerSchema("Client User identifier")]
    public UserResource User { get; set; }
}