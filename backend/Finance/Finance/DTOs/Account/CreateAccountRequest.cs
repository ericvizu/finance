using System.ComponentModel.DataAnnotations;

namespace Finance.DTOs.Account;

public record CreateAccountRequest(
    [Required]
    [StringLength(100, MinimumLength = 3)]
    string Name
);