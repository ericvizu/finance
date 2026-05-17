using Finance.DTOs;
using Finance.Entities;
using Finance.Interfaces;

namespace Finance.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponse?> RegisterAsync(UserRegistrationRequest request)
    {
        // Checks if user with that email already exists
        if (await _userRepository.ExistsByEmailAsync(request.Email))
        {
            return null;
        }

        // Instantiates new user, using attributes from the DTO
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
        };

        // Stages and commit changes to PostgreSQL
        await _userRepository.AddAsync(user);
        var success = await _userRepository.SaveChangesAsync();

        if (!success)
        {
            return null;
        }

        // If successful, returns the corresponding DTO
        return new UserResponse(
            user.Id,
            user.Name,
            user.Email,
            user.CreatedAt);
    }

    public async Task<UserResponse?> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user != null)
            return new UserResponse(
                user.Id,
                user.Name,
                user.Email,
                user.CreatedAt);
        return null;
    }
}