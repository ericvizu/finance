namespace Finance.DTOs.Account;

public record AccountResponse(
    Guid Id,
    string Name,
    DateTime CreatedAt
);