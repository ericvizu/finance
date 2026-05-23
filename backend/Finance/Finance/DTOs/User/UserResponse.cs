namespace Finance.DTOs.User;

// Public profile data returned by the API
// Omits sensitive data, like password
public record UserResponse(
    Guid Id,
    string Name,
    string Email,
    DateTime CreatedAt
);