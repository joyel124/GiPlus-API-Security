using GiPlus.API.Security.Authorization.Handlers.Implementations;
using GiPlus.API.Security.Authorization.Handlers.Interfaces;
using GiPlus.API.Security.Authorization.Middleware;
using GiPlus.API.Security.Authorization.Settings;
using GiPlus.API.Security.Domain.Repositories;
using GiPlus.API.Security.Domain.Services;
using GiPlus.API.Security.Mapping;
using GiPlus.API.Security.Persistence.Repositories;
using GiPlus.API.Security.Services;
using GiPlus.API.Shared.Domain.Repositories;
using GiPlus.API.Shared.Persistence.Contexts;
using GiPlus.API.Shared.Persistence.Repositories;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add CORS Service
builder.Services.AddCors();

// AppSettings Configuration
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddSwaggerGen(options =>
    {
        //Add API Documentation Information
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "GI-PLUS API",
            Description = "GI-PLUS RESTful API",
            TermsOfService = new Uri("https://gi-plus.com/tos"),
            Contact = new OpenApiContact
            {
                Name = "GIPLUS.studio",
                Url = new Uri("https://gi-plus.studio")
            },
            License = new OpenApiLicense
            {
                Name = "Gi Plus Resources License",
                Url = new Uri("https://gi-plus.com/license")
            }
        });
        options.EnableAnnotations();
        options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "JWT Authorization header using the Bearer Scheme."
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                },
                Array.Empty<string>()
            }
        });
    }
    
    );

// Add DataBase Connection
var connectionString=builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(
    options=>options.UseMySQL(connectionString)
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors());

//Add lowercase routes
builder.Services.AddRouting(options=>options.LowercaseUrls=true);

//Dependency Injection Configuration

// Security Injection Configuration
builder.Services.AddScoped<IJwtHandler, JwtHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Shared Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// AutoMapper Configuration
builder.Services.AddAutoMapper(
    typeof(ModelToResourceProfile),
    typeof(ResourceToModelProfile));

var app = builder.Build();

//Validation for ensuring Database Objects are created
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("v1/swagger.json","v1");
        options.RoutePrefix = "swagger";
    });
}

// Configure CORS
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Middleware Services Configuration

// Configure Error Handler Middleware
app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure JSON Web Token Handling Middleware
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();