using GiPlus.API.Security.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Management.Resources;

public class EmployeeResource
{
    [SwaggerSchema("Employee identifier")]
    public int Id { get; set; }
    [SwaggerSchema("Employee Document Type")]
    public string DocumentType { get; set; }
    [SwaggerSchema("Employee Number Identification")]
    public int NumberIdentification { get; set; }
    [SwaggerSchema("Employee FirstName")]
    public string FirstName { get; set; }
    [SwaggerSchema("Employee LastName")]
    public string LastName { get; set; }
    [SwaggerSchema("Employee Phone")]
    public int Phone { get; set; }
    [SwaggerSchema("Employee Email")]
    public string Email { get; set; }
    [SwaggerSchema("Employee City")]
    public string City { get; set; }
    [SwaggerSchema("Employee Address")]
    public string Address { get; set; }
    [SwaggerSchema("Employee Job Position")]
    public string JobPosition { get; set; }
    [SwaggerSchema("Employee User identifier")]
    public UserResource User { get; set; }
}