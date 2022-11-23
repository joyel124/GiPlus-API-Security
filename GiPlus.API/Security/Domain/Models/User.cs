using System.Text.Json.Serialization;
using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Sales.Domain.Models;

namespace GiPlus.API.Security.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    [JsonIgnore]
    public string PasswordHash { get; set; }
    
    public IList<Client> Clients { get; set; }
    public IList<Supplier> Suppliers { get; set; }
    public IList<Employee> Employees { get; set; }
    public IList<Product> Products { get; set; }
    public IList<Sale> Sales { get; set; }
    public IList<Request> Requests { get; set; }
    
}