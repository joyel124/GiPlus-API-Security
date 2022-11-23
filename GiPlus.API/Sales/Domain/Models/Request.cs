using GiPlus.API.Security.Domain.Models;

namespace GiPlus.API.Sales.Domain.Models;

public class Request
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public string Date { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
    public int ClientId { get; set; }
    
    //Relationsships
    public int UserId { get; set; }
    public User User { get; set; }
}