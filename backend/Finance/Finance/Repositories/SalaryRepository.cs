using Finance.Data;
using Finance.Entities;
using Finance.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Finance.Repositories;

public class SalaryRepository : ISalaryRepository
{
    private readonly AppDbContext _context;
    
    public SalaryRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Salary?> GetByIdAsync(Guid id, Guid userId)
    {
        return await _context.Salaries.FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId );
    }

    public async Task<IEnumerable<Salary>> GetAllByUserIdAsync(Guid userId)
    {
        return  await _context.Salaries
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.StartDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Salary>> GetActiveByUserIdAsync(Guid userId)
    {
        return  await _context.Salaries
            .Where(s => s.UserId == userId && s.EndDate == null)
            .OrderByDescending(s => s.StartDate)
            .ToListAsync();
    }

    public void Add(Salary salary)
    {
        _context.Salaries.Add(salary);
    }

    public void Remove(Salary salary)
    {
        _context.Salaries.Remove(salary);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}