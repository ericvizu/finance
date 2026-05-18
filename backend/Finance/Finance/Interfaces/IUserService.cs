using Finance.DTOs;

namespace Finance.Interfaces;

public interface IUserService
{
    Task<UserResponse?> RegisterAsync(UserRegistrationRequest request);
    Task<UserResponse?> GetByIdAsync(Guid id);
    Task<UserResponse?> UpdateByIdAsync(Guid id, UserUpdateRequest request);
    Task<bool> DeleteByIdAsync(Guid id);
}