using System.ComponentModel.DataAnnotations;

namespace Finance.DTOs.User;

// For creating a new user
// Password in text, to be hashed in the Service layer
public record UserRegistrationRequest(
    [Required(ErrorMessage = "Name obligatory")]
    [StringLength(100, MinimumLength = 3)]
    string Name,

    [Required]
    [EmailAddress]
    [StringLength(150)]
    string Email,

    [Required]
    [StringLength(50, MinimumLength = 5)]
    string Password
);