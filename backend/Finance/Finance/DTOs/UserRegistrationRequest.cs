namespace Finance.DTOs;

// For creating a new user
// Password in text, to be hashed in the Service layer
public record UserRegistrationRequest(
    string Name,
    string Email,
    string Password
);