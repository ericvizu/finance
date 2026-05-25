using Finance.Entities;
using Finance.Enums;
using Microsoft.EntityFrameworkCore;

namespace Finance.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    // Represents the physical 'Users' table in the database
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Salary> Salaries { get; set; }

    // Configuration for the database schema using Fluent API.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            // PK using UUID v7
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id)
                .HasDefaultValueSql("uuidv7()");

            // Name configurations
            entity.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Email configurations
            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);
            
            entity.HasIndex(u => u.Email)
                .IsUnique(); // Email must be unique

            // Password configurations
            entity.Property(u => u.PasswordHash)
                .IsRequired();

            // CreatedAt configurations, auto set to now
            entity.Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            // No OnModelCreating do AppDbContext.cs
            entity.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(50)
                .HasConversion<string>() 
                .HasDefaultValue(UserRole.User);
        });
        modelBuilder.Entity<Account>(entity =>
        {
            // PK using UUID v7
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id)
                .HasDefaultValueSql("uuidv7()");

            // Name configurations
            entity.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            // CreatedAt configurations, auto set to now
            entity.Property(a => a.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            entity.HasOne(a => a.User)
                .WithMany(u => u.Accounts)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<Salary>(entity =>
        {
            // PK using UUID v7
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Id)
                .HasDefaultValueSql("uuidv7()");

            // Name configurations
            entity.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Monetary values (Precision is mandatory for PostgreSQL to avoid truncation)
            entity.Property(s => s.GrossAmount)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(s => s.NetAmount)
                .HasPrecision(18, 2)
                .IsRequired();

            // CreatedAt configurations, auto set to now
            entity.Property(s => s.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Relationship
            entity.HasOne(s => s.User)
                .WithMany(u => u.Salaries) 
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}