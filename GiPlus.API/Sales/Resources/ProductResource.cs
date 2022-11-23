using GiPlus.API.Security.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Sales.Resources;

public class ProductResource
{
    [SwaggerSchema("Product identifier")]
    public int Id { get; set; }
    [SwaggerSchema("Product Name")]
    public string Name { get; set; }
    [SwaggerSchema("Product Brand")]
    public string Brand { get; set; }
    [SwaggerSchema("Product Description")]
    public string Description { get; set; }
    [SwaggerSchema("Product Price")]
    public int Price { get; set; }
    [SwaggerSchema("Product Quantity")]
    public int Quantity { get; set; }
    
    [SwaggerSchema("Product User identifier")]
    public UserResource User { get; set; }
}