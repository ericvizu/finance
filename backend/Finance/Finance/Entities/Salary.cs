namespace Finance.Entities;

public class Salary
{
    public Guid Id { get; set; } // PK
    
    public Guid UserId { get; set; } // FK
    public User User { get; set; } = null!; 
    
    public string Name { get; set; } = string.Empty;
    
    public decimal GrossAmount { get; set; }
    public decimal NetAmount { get; set; }
    
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; } 
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}