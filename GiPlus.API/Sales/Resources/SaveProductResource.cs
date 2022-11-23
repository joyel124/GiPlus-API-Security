using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Sales.Resources;

[SwaggerSchema(Required = new[]{"Name"})]
public class SaveProductResource
{
    [Required]
    [SwaggerSchema("Product Name")]
    public string Name { get; set; }
    
    [Required]
    [SwaggerSchema("Product Brand")]
    public string Brand { get; set; }
    
    [Required]
    [SwaggerSchema("Product Description")]
    public string Description { get; set; }
    
    [Required]
    [SwaggerSchema("Product Price")]
    public int Price { get; set; }
    
    [Required]
    [SwaggerSchema("Product Quantity")]
    public int Quantity { get; set; }

    [Required]
    [SwaggerSchema("Product UserId")]
    public int UserId { get; set; }
}