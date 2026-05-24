using Finance.DTOs.Account;
using Finance.Entities;
using Finance.Interfaces;

namespace Finance.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _repository;

    public AccountService(IAccountRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<AccountResponse?> CreateAsync(CreateAccountRequest request, Guid userId)
    {
        var accountExists =  await _repository.ExistsByNameAsync(request.Name, userId);
        if (accountExists)
        {
            return null;
        }

        var account = new Account()
        {
            UserId = userId,
            Name = request.Name,
            CreatedAt = DateTime.UtcNow
        };
        
        _repository.Add(account);
        await _repository.SaveChangesAsync();
        
        return new AccountResponse(account.Id, account.Name, account.CreatedAt);
    }

    public async Task<IEnumerable<AccountResponse>> GetAllByUserIdAsync(Guid userId)
    { 
        var accounts = await _repository.GetAllByUserIdAsync(userId);
        return accounts.Select(a => new AccountResponse(a.Id, a.Name, a.CreatedAt));
    }

    public async Task<AccountResponse?> GetByIdAsync(Guid id, Guid userId)
    {
        var account = await _repository.GetByIdAsync(id, userId);
        return account == null ? null : new AccountResponse(account.Id, account.Name, account.CreatedAt);
    }

    public async Task<AccountResponse?> UpdateByIdAsync(Guid id, UpdateAccountRequest request, Guid userId)
    {
        var account = await _repository.GetByIdAsync(id, userId);
        if (account == null)
        {
            return null;
        }
        
        account.Name = request.Name;
        account.UpdatedAt = DateTime.UtcNow;
        
        await _repository.SaveChangesAsync();
        return new AccountResponse(account.Id, account.Name, account.CreatedAt);
        
    }

    public async Task<bool> DeleteByIdAsync(Guid id, Guid userId)
    {
        var  account = await _repository.GetByIdAsync(id, userId);
        if (account == null)
        {
            return false;
        }
        _repository.Remove(account);
        await _repository.SaveChangesAsync();
        return true;
    }
}