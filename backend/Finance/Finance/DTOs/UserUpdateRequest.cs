using System.ComponentModel.DataAnnotations;

namespace Finance.DTOs;

// For updating user profiles
public record UserUpdateRequest(
    [Required]
    [StringLength(100, MinimumLength = 3)]
    string Name
);