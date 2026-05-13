using Finance.Entities;
using Microsoft.EntityFrameworkCore;

namespace Finance.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    // Represents the physical 'Users' table in the database
    public DbSet<User> Users { get; set; }

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
        });
    }
}