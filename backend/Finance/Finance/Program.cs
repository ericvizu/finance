using System.Text;
using Finance.Data;
using Finance.Interfaces;
using Finance.Repositories;
using Finance.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Finance;

public class Program
{
    public static void Main(string[] args)
    {
        // Initializes the web application builder, loading appsettings.json and environment variables
        var builder = WebApplication.CreateBuilder(args);
        
        // --- 1. AUTHENTICATION ---
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]!);
        
        builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false; // Mude para true em produção
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // Remove o atraso padrão de 5min do token
                };
            });

        builder.Services.AddAuthorization();
        
        // 1. Swagger/OpenAPI Setup
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(); // Required for Swagger UI generation

        // Retrieves the connection string from appsettings.json
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // Registers the AppDbContext with the Npgsql provider for PostgreSQL
        builder.Services.AddDbContext<AppDbContext>(options => 
            options.UseNpgsql(connectionString));
            
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<ISalaryRepository, SalaryRepository>();
        builder.Services.AddScoped<ISalaryService, SalaryService>();
                
        // Builds the application
        var app = builder.Build();
        
        // --- HTTP REQUEST PIPELINE ---
        // 2. Enable Swagger UI in Development
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(); // This enables the /swagger page
        }
        
        // Authentication
        app.UseAuthentication();
        app.UseAuthorization();

        // Maps controller routes so the API knows which code to execute for specific URLs
        app.MapControllers();

        // Starts the application and listens for requests
        app.Run();
    }
}