using GiPlus.API.Management.Domain.Models;
using GiPlus.API.Sales.Domain.Models;
using GiPlus.API.Security.Domain.Models;
using GiPlus.API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GiPlus.API.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Sale> Sales { get; set; }

    public AppDbContext(DbContextOptions options) : base(options){
    }
    protected override void OnModelCreating(ModelBuilder builder){
        base.OnModelCreating(builder);
        //USERS
        //Constraints
        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(p=>p.Id);
        builder.Entity<User>().Property(p=>p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(p=>p.FirstName).IsRequired();
        builder.Entity<User>().Property(p=>p.LastName).IsRequired();
        builder.Entity<User>().Property(p=>p.Email).IsRequired().HasMaxLength(30);
        
        //CLIENTS
        builder.Entity<Client>().ToTable("Clients");
        builder.Entity<Client>().HasKey(p=>p.Id);
        builder.Entity<Client>().Property(p=>p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Client>().Property(p=>p.DocumentType).IsRequired();
        builder.Entity<Client>().Property(p=>p.NumberIdentification).IsRequired();
        builder.Entity<Client>().Property(p=>p.FirstName).IsRequired();
        builder.Entity<Client>().Property(p=>p.LastName).IsRequired();
        builder.Entity<Client>().Property(p=>p.Phone).IsRequired();
        builder.Entity<Client>().Property(p=>p.Email).IsRequired().HasMaxLength(30);
        builder.Entity<Client>().Property(p=>p.City).IsRequired();
        builder.Entity<Client>().Property(p=>p.Address).IsRequired();

        //EMPLOYEES
        builder.Entity<Employee>().ToTable("Employees");
        builder.Entity<Employee>().HasKey(p=>p.Id);
        builder.Entity<Employee>().Property(p=>p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Employee>().Property(p=>p.DocumentType).IsRequired();
        builder.Entity<Employee>().Property(p=>p.NumberIdentification).IsRequired();
        builder.Entity<Employee>().Property(p=>p.FirstName).IsRequired();
        builder.Entity<Employee>().Property(p=>p.LastName).IsRequired();
        builder.Entity<Employee>().Property(p=>p.Phone).IsRequired();
        builder.Entity<Employee>().Property(p=>p.Email).IsRequired().HasMaxLength(30);
        builder.Entity<Employee>().Property(p=>p.City).IsRequired();
        builder.Entity<Employee>().Property(p=>p.Address).IsRequired();
        builder.Entity<Employee>().Property(p=>p.JobPosition).IsRequired();
        
        //SUPPLIERS
        builder.Entity<Supplier>().ToTable("Suppliers");
        builder.Entity<Supplier>().HasKey(p=>p.Id);
        builder.Entity<Supplier>().Property(p=>p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Supplier>().Property(p=>p.Name).IsRequired();
        builder.Entity<Supplier>().Property(p=>p.Phone).IsRequired();
        builder.Entity<Supplier>().Property(p=>p.Email).IsRequired().HasMaxLength(30);
        builder.Entity<Supplier>().Property(p=>p.Address).IsRequired();
        builder.Entity<Supplier>().Property(p=>p.Ruc).IsRequired();
        
        //PRODUCTS
        builder.Entity<Product>().ToTable("Products");
        builder.Entity<Product>().HasKey(p=>p.Id);
        builder.Entity<Product>().Property(p=>p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Product>().Property(p=>p.Name).IsRequired();
        builder.Entity<Product>().Property(p=>p.Brand).IsRequired();
        builder.Entity<Product>().Property(p=>p.Description).IsRequired();
        builder.Entity<Product>().Property(p=>p.Price).IsRequired();
        builder.Entity<Product>().Property(p=>p.Quantity).IsRequired();
        
        //REQUESTS
        builder.Entity<Request>().ToTable("Requests");
        builder.Entity<Request>().HasKey(p=>p.Id);
        builder.Entity<Request>().Property(p=>p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Request>().Property(p=>p.Name).IsRequired();
        builder.Entity<Request>().Property(p=>p.Brand).IsRequired();
        builder.Entity<Request>().Property(p=>p.Status).IsRequired();
        builder.Entity<Request>().Property(p=>p.Description).IsRequired();
        builder.Entity<Request>().Property(p=>p.ClientId).IsRequired();
        
        //SALES
        builder.Entity<Sale>().ToTable("Sales");
        builder.Entity<Sale>().HasKey(p=>p.Id);
        builder.Entity<Sale>().Property(p=>p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Sale>().Property(p=>p.Date).IsRequired();
        builder.Entity<Sale>().Property(p=>p.PaymentVoucher).IsRequired();
        builder.Entity<Sale>().Property(p=>p.SaleDetails).IsRequired();
        builder.Entity<Sale>().Property(p=>p.ClientId).IsRequired();

        //Relationships
        builder.Entity<User>()
            .HasMany(p => p.Clients)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
        
        builder.Entity<User>()
            .HasMany(p => p.Employees)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
        
        builder.Entity<User>()
            .HasMany(p => p.Suppliers)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
        
        builder.Entity<User>()
            .HasMany(p => p.Products)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
        
        builder.Entity<User>()
            .HasMany(p => p.Sales)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
        
        builder.Entity<User>()
            .HasMany(p => p.Requests)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);

        //Apply Snake Case Naming Convention

        builder.UseSnakeCaseNamingConvention();
    }
    
    
}