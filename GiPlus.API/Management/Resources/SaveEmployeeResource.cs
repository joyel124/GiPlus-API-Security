using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Management.Resources;

[SwaggerSchema(Required = new[]{"Name"})]
public class SaveEmployeeResource
{
    [Required]
    [SwaggerSchema("Employee Document Type")]
    public string DocumentType { get; set; }
    
    [Required]
    [SwaggerSchema("Employee Number Identification")]
    public int NumberIdentification { get; set; }
    
    [Required]
    [SwaggerSchema("Employee FirstName")]
    public string FirstName { get; set; }
    
    [Required]
    [SwaggerSchema("Employee LastName")]
    public string LastName { get; set; }
    
    [Required]
    [SwaggerSchema("Employee Phone")]
    public int Phone { get; set; }
    
    [Required]
    [SwaggerSchema("Employee Email")]
    public string Email { get; set; }
    
    [Required]
    [SwaggerSchema("Employee City")]
    public string City { get; set; }
    
    [Required]
    [SwaggerSchema("Employee Address")]
    public string Address { get; set; }
    
    [Required]
    [SwaggerSchema("Employee Job Position")]
    public string JobPosition { get; set; }
    
    [Required]
    [SwaggerSchema("Employee UserId")]
    public int UserId { get; set; }
}