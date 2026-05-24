using Finance.Data;
using Finance.Entities;
using Finance.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Finance.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetByIdAsync(Guid id, Guid userId)
    {
        return await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
    }

    public async Task<IEnumerable<Account>> GetAllByUserIdAsync(Guid userId)
    {
        return await _context.Accounts.Where(a => a.UserId == userId).ToListAsync();
    }

    public void Add(Account account)
    {
        _context.Accounts.AddAsync(account);
    }

    public async Task<bool> ExistsByNameAsync(string name, Guid userId)
    {
        return await _context.Accounts.AnyAsync(a => a.UserId == userId && a.Name == name);
    }

    public void Remove(Account account)
    {
        _context.Accounts.Remove(account);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}