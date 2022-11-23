using GiPlus.API.Security.Domain.Models;

namespace GiPlus.API.Management.Domain.Models;

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Phone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public int Ruc { get; set; }
    
    //Relationsships
    public int UserId { get; set; }
    public User User { get; set; }
}