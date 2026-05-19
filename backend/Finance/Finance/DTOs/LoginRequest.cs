using System.ComponentModel.DataAnnotations;

namespace Finance.DTOs;

public record LoginRequest(
    [Required(ErrorMessage = "E-mail is mandatory.")]
    [EmailAddress(ErrorMessage = "Invalid e-mail address.")]
    string Email,

    [Required(ErrorMessage = "Password is mandatory.")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 50 characters.")]
    string Password
);