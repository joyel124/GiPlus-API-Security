using GiPlus.API.Security.Domain.Models;
using GiPlus.API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GiPlus.API.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
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

        //Relationships
        

        //Apply Snake Case Naming Convention

        builder.UseSnakeCaseNamingConvention();
    }
    
    
}