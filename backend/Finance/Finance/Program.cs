using Finance.Data;
using Finance.Interfaces;
using Finance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Finance;

public class Program
{
    public static void Main(string[] args)
    {
        // Initializes the web application builder, loading appsettings.json and environment variables
        var builder = WebApplication.CreateBuilder(args);
        
        
        // --- SERVICE REGISTRATION (Dependency Injection) ---
        // Registers controllers to handle incoming HTTP requests
        builder.Services.AddControllers();

        // Retrieves the connection string from appsettings.json
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // Registers the AppDbContext with the Npgsql provider for PostgreSQL
        builder.Services.AddDbContext<AppDbContext>(options => 
            options.UseNpgsql(connectionString));
        builder.Services.AddScoped<IUserRepository, UserRepository>();
                
        // Builds the application
        var app = builder.Build();
        
        
        // --- HTTP REQUEST PIPELINE ---
        // Maps controller routes so the API knows which code to execute for specific URLs
        app.MapControllers();

        // Starts the application and listens for requests
        app.Run();
    }
}