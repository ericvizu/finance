using Finance.DTOs.Salary;
using Finance.Entities;
using Finance.Interfaces;

namespace Finance.Services;

public class SalaryService : ISalaryService
{
    private readonly ISalaryRepository _repository;

    public SalaryService(ISalaryRepository repository)
    {
        _repository = repository;
    }

    public async Task<SalaryResponse?> CreateAsync(CreateSalaryRequest request, Guid userId)
    {
        ValidateDates(request.StartDate, request.EndDate);

        var salary = new Salary
        {
            UserId = userId,
            Name = request.Name,
            GrossAmount = request.GrossAmount,
            NetAmount = request.NetAmount,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };
        
        _repository.Add(salary);
        await _repository.SaveChangesAsync();
        return MapToResponse(salary);
    }

    public async Task<IEnumerable<SalaryResponse>> GetAllByUserIdAsync(Guid userId)
    {
        var response = await _repository.GetAllByUserIdAsync(userId);
        return response.Select(MapToResponse);
    }

    public async Task<IEnumerable<SalaryResponse>> GetActiveByUserIdAsync(Guid userId)
    {
        var response = await _repository.GetActiveByUserIdAsync(userId);
        return response.Select(MapToResponse);
    }

    public async Task<SalaryResponse?> GetByIdAsync(Guid id, Guid userId)
    {
        var result =  await _repository.GetByIdAsync(id, userId);
        return result == null ? null : MapToResponse(result);
    }

    public async Task<SalaryResponse?> UpdateByIdAsync(Guid id, UpdateSalaryRequest request, Guid userId)
    {
        ValidateDates(request.StartDate, request.EndDate);

        var salary = await _repository.GetByIdAsync(id, userId);
        if (salary is null) return null;

        salary.Name = request.Name;
        salary.GrossAmount = request.GrossAmount;
        salary.NetAmount = request.NetAmount;
        salary.StartDate = request.StartDate;
        salary.EndDate = request.EndDate;
        
        salary.UpdatedAt = DateTime.UtcNow;

        await _repository.SaveChangesAsync();

        return MapToResponse(salary);
    }

    public async Task<bool> DeleteByIdAsync(Guid id, Guid userId)
    {
        var salary = await _repository.GetByIdAsync(id, userId);
        if (salary is null) return false;

        _repository.Remove(salary);
        return await _repository.SaveChangesAsync();
    }
    
    // Auxiliary method

    private static SalaryResponse MapToResponse(Salary salary)
    {
        return new SalaryResponse(
            salary.Id,
            salary.Name,
            salary.GrossAmount,
            salary.NetAmount,
            salary.StartDate,
            salary.EndDate
        );
    }
    
    private static void ValidateDates(DateOnly startDate, DateOnly? endDate)
    {
        if (endDate < startDate)
            throw new ArgumentException("End date cannot be earlier than start date.");
    }
}