using Finance.DTOs;

namespace Finance.Interfaces;

public interface IUserService
{
    Task<UserResponse?> RegisterAsync(UserRegistrationRequest request);
}