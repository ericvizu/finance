using Finance.Entities;

namespace Finance.Interfaces;

public interface ISalaryRepository
{
    Task<Salary?> GetByIdAsync(Guid id, Guid userId);
    Task<IEnumerable<Salary>> GetAllByUserIdAsync(Guid userId);
    Task<IEnumerable<Salary>> GetActiveByUserIdAsync(Guid userId);
    
    void Add(Salary salary);
    void Remove(Salary salary);
    
    Task<bool> SaveChangesAsync();
}