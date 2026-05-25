using System.ComponentModel.DataAnnotations;

namespace Finance.DTOs.Salary;

public record UpdateSalaryRequest(
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    string Name,

    [Required(ErrorMessage = "Gross amount is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Gross amount must be greater than zero.")]
    decimal GrossAmount,

    [Required(ErrorMessage = "Net amount is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Net amount must be greater than zero.")]
    decimal NetAmount,

    [Required(ErrorMessage = "Start date is required.")]
    DateOnly StartDate,

    DateOnly? EndDate
);