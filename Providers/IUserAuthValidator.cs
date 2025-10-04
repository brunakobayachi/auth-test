using System.Security.Claims;

namespace Providers;

public interface IUserAuthValidator
{
    (string user, string password) ParseBasicToken(string token);
    Task<(bool isValid, IEnumerable<Claim> claims)> ValidateAsync(string username, string password, CancellationToken ct = default);
}
