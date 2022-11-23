using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Management.Resources;

[SwaggerSchema(Required = new[]{"Name"})]
public class SaveSupplierResource
{
    [Required]
    [SwaggerSchema("Supplier Name")]
    public string Name { get; set; }
    
    [Required]
    [SwaggerSchema("Supplier Phone")]
    public int Phone { get; set; }
    
    [Required]
    [SwaggerSchema("Supplier Email")]
    public string Email { get; set; }
    
    [Required]
    [SwaggerSchema("Supplier Address")]
    public string Address { get; set; }
    
    [Required]
    [SwaggerSchema("Supplier Ruc")]
    public int Ruc { get; set; }
    
    [Required]
    [SwaggerSchema("Supplier UserId")]
    public int UserId { get; set; }
}