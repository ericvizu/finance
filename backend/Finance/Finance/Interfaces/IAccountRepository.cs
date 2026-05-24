using Finance.Entities;

namespace Finance.Interfaces;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid id, Guid userId);
    Task<IEnumerable<Account>> GetAllByUserIdAsync(Guid userId);
    void Add(Account account);
    Task<bool> ExistsByNameAsync(string name, Guid userId);
    void Remove(Account account);
    
    Task<bool> SaveChangesAsync();

}