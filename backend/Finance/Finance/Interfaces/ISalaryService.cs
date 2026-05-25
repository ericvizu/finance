using Finance.DTOs.Salary;

namespace Finance.Interfaces;

public interface ISalaryService
{
    Task<SalaryResponse?> CreateAsync(CreateSalaryRequest request, Guid userId);
    Task<IEnumerable<SalaryResponse>> GetAllByUserIdAsync(Guid userId);
    Task<IEnumerable<SalaryResponse>> GetActiveByUserIdAsync(Guid userId);
    Task<SalaryResponse?> GetByIdAsync(Guid id, Guid userId);
    Task<SalaryResponse?> UpdateByIdAsync(Guid id, UpdateSalaryRequest request, Guid userId);
    Task<bool> DeleteByIdAsync(Guid id, Guid userId); 
}