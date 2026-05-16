using Finance.Data;
using Finance.Interfaces;
using Finance.Repositories;
using Finance.Services;
using Microsoft.EntityFrameworkCore;

namespace Finance;

public class Program
{
    public static void Main(string[] args)
    {
        // Initializes the web application builder, loading appsettings.json and environment variables
        var builder = WebApplication.CreateBuilder(args);
        
        // --- SERVICE REGISTRATION (Dependency Injection) ---
        builder.Services.AddControllers();

        // 1. Swagger/OpenAPI Setup
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(); // Required for Swagger UI generation

        // Retrieves the connection string from appsettings.json
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // Registers the AppDbContext with the Npgsql provider for PostgreSQL
        builder.Services.AddDbContext<AppDbContext>(options => 
            options.UseNpgsql(connectionString));
            
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
                
        // Builds the application
        var app = builder.Build();
        
        // --- HTTP REQUEST PIPELINE ---
        // 2. Enable Swagger UI in Development
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(); // This enables the /swagger page
        }

        // Maps controller routes so the API knows which code to execute for specific URLs
        app.MapControllers();

        // Starts the application and listens for requests
        app.Run();
    }
}