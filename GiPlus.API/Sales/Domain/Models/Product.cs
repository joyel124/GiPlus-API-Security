using GiPlus.API.Security.Domain.Models;

namespace GiPlus.API.Sales.Domain.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    
    //Relationsships
    public int UserId { get; set; }
    public User User { get; set; }
}