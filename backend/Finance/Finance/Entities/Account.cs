namespace Finance.Entities;

public class Account
{
    public Guid Id { get; set; } // PK
    public Guid UserId { get; set; } // FK from User
    public User User { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}