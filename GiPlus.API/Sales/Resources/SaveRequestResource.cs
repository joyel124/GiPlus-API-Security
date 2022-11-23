using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Sales.Resources;

[SwaggerSchema(Required = new[]{"Name"})]
public class SaveRequestResource
{
    [Required]
    [SwaggerSchema("Request Name")]
    public string Name { get; set; }
    
    [Required]
    [SwaggerSchema("Request Brand")]
    public string Brand { get; set; }
    
    [Required]
    [SwaggerSchema("Request Date")]
    public string Date { get; set; }
    
    [Required]
    [SwaggerSchema("Request Status")]
    public string Status { get; set; }
    
    [Required]
    [SwaggerSchema("Request Description")]
    public string Description { get; set; }
    
    [Required]
    [SwaggerSchema("Request ClientId")]
    public int ClientId { get; set; }
    
    [Required]
    [SwaggerSchema("Request UserId")]
    public int UserId { get; set; }
}