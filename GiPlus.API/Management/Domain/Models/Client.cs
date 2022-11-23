using GiPlus.API.Security.Domain.Models;

namespace GiPlus.API.Management.Domain.Models;

public class Client
{
    public int Id { get; set; }
    public string DocumentType { get; set; }
    public int NumberIdentification { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Phone { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    
    //Relationsships
    public int UserId { get; set; }
    public User User { get; set; }
}