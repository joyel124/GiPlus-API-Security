using GiPlus.API.Security.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Sales.Resources;

public class RequestResource
{
    [SwaggerSchema("Request identifier")]
    public int Id { get; set; }
    [SwaggerSchema("Request Name")]
    public string Name { get; set; }
    [SwaggerSchema("Request Brand")]
    public string Brand { get; set; }
    [SwaggerSchema("Request Date")]
    public string Date { get; set; }
    [SwaggerSchema("Request Status")]
    public string Status { get; set; }
    [SwaggerSchema("Request Description")]
    public string Description { get; set; }
    [SwaggerSchema("Request ClientId")]
    public int ClientId { get; set; }
    [SwaggerSchema("Request User identifier")]
    public UserResource User { get; set; }
}