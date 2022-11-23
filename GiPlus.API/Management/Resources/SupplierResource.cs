using GiPlus.API.Security.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Management.Resources;

public class SupplierResource
{
    [SwaggerSchema("Supplier identifier")]
    public int Id { get; set; }
    [SwaggerSchema("Supplier Name")]
    public string Name { get; set; }
    [SwaggerSchema("Supplier Phone")]
    public int Phone { get; set; }
    [SwaggerSchema("Supplier Email")]
    public string Email { get; set; }
    [SwaggerSchema("Supplier Address")]
    public string Address { get; set; }
    [SwaggerSchema("Supplier Ruc")]
    public int Ruc { get; set; }
    [SwaggerSchema("Supplier User identifier")]
    public UserResource User { get; set; }
}