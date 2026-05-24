using Finance.DTOs.Account;

namespace Finance.Interfaces;

public interface IAccountService
{
    Task<AccountResponse?> CreateAsync(CreateAccountRequest request, Guid userId);
    Task<IEnumerable<AccountResponse>> GetAllByUserIdAsync(Guid userId);
    Task<AccountResponse?> GetByIdAsync(Guid id, Guid userId);
    Task<AccountResponse?> UpdateByIdAsync(Guid id, UpdateAccountRequest request, Guid userId);
    Task<bool> DeleteByIdAsync(Guid id, Guid userId); 
}