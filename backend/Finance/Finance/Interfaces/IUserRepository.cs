using Finance.Entities;

namespace Finance.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
    Task<bool> ExistsByEmailAsync(string email);
    void Remove(User user);
    
    Task<bool> SaveChangesAsync();
}