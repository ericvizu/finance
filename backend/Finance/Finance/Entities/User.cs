namespace Finance.Entities;

public class User
{
    public Guid Id { get; set; } // Primary Key - Uses uuidv7() for sequentiality
    public string Name { get; set; } = string.Empty; // User's full name
    public string Email { get; set; } = string.Empty; // Unique contact email
    public string PasswordHash { get; set; } = string.Empty; // Encrypted password storage
    public DateTime CreatedAt { get; set; } // Record creation timestamp
    public DateTime? UpdatedAt { get; set; } // Last modification timestamp (nullable)
    public string Role { get; set; } = "User"; // Role for authentication
}