using System.ComponentModel.DataAnnotations;

namespace Finance.DTOs.Account;

public record UpdateAccountRequest(
    [Required]
    [StringLength(100, MinimumLength = 3)]
    string Name
);