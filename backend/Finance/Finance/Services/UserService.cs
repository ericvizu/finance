using Finance.DTOs;
using Finance.DTOs.User;
using Finance.Entities;
using Finance.Interfaces;

namespace Finance.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public UserService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
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

    public async Task<UserResponse?> UpdateByIdAsync(Guid id, UserUpdateRequest request)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return null;
        }
        user.Name = request.Name;
        user.UpdatedAt = DateTime.UtcNow;
        
        await _userRepository.SaveChangesAsync();
        return new UserResponse(
            user.Id,
            user.Name,
            user.Email,
            user.CreatedAt);
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return false;
        }
        _userRepository.Remove(user);
        await _userRepository.SaveChangesAsync();
        return true;
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        var result = await _userRepository.GetByEmailAsync(email);
        if (result == null || !BCrypt.Net.BCrypt.Verify(password, result.PasswordHash))
        {
            return null;
        }

        return _tokenService.GenerateToken(result);
    }
}