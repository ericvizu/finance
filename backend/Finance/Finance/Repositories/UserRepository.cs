using Finance.Data;
using Finance.Entities;
using Finance.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Finance.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }
    public async Task<User?> GetByEmailAsync(string email)
    {
        // Can't use FindAsync because the method is reserved for PK
        // And the email is not a PK, so it would result in an error
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}