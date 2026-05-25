namespace Finance.DTOs.Salary;

public record SalaryResponse(
    Guid Id,
    string Name,
    decimal GrossAmount,
    decimal NetAmount,
    DateOnly StartDate,
    DateOnly? EndDate
);