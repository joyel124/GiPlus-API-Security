using GiPlus.API.Security.Domain.Models;

namespace GiPlus.API.Management.Domain.Models;

public class Employee
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
    public string JobPosition { get; set; }
    
    //Relationsships
    public int UserId { get; set; }
    public User User { get; set; }
}