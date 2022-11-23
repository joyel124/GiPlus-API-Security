using GiPlus.API.Security.Domain.Models;

namespace GiPlus.API.Sales.Domain.Models;

public class Sale
{
    public int Id { get; set; }
    public string Date { get; set; }
    public string PaymentVoucher { get; set; }
    public string SaleDetails { get; set; }
    public int ClientId { get; set; }
    
    //Relationsships
    public int UserId { get; set; }
    public User User { get; set; }
}