using GiPlus.API.Security.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace GiPlus.API.Sales.Resources;

public class SaleResource
{
    [SwaggerSchema("Sale identifier")]
    public int Id { get; set; }
    [SwaggerSchema("Sale Date")]
    public string Date { get; set; }
    [SwaggerSchema("Sale Payment Voucher")]
    public string PaymentVoucher { get; set; }
    [SwaggerSchema("Sale Sale Details")]
    public string SaleDetails { get; set; }
    [SwaggerSchema("Sale ClientId")]
    public int ClientId { get; set; }
    [SwaggerSchema("Sale User identifier")]
    public UserResource User { get; set; }
}