using Finance.Entities;

namespace Finance.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}