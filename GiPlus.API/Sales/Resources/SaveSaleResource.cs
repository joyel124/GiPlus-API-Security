using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Sales.Resources;

[SwaggerSchema(Required = new[]{"Name"})]
public class SaveSaleResource
{
    [Required]
    [SwaggerSchema("Sale Date")]
    public string Date { get; set; }
    
    [Required]
    [SwaggerSchema("Sale Payment Voucher")]
    public string PaymentVoucher { get; set; }
    
    [Required]
    [SwaggerSchema("Sale Sale Details")]
    public string SaleDetails { get; set; }
    
    [Required]
    [SwaggerSchema("Sale ClientId")]
    public int ClientId { get; set; }
    
    [Required]
    [SwaggerSchema("Sale UserId")]
    public int UserId { get; set; }
}