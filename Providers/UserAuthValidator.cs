using System.Security.Claims;
using System.Text;
using FastEndpointsBasicAuthEf.Data;
using Microsoft.EntityFrameworkCore;
using Providers;

namespace FastEndpointsBasicAuthEf.Services;

public class UserAuthValidator : IUserAuthValidator
{
    private readonly AppDbContext _db;
    public UserAuthValidator(AppDbContext db) => _db = db;

    public (string user, string password) ParseBasicToken(string token)
    {
        var convertedBytes = Convert.FromBase64String(token);
        var convertedString = Encoding.UTF8.GetString(convertedBytes);

        var parts = convertedString.ToString().Split(":", 2);

        var user = parts[0];
        var password = parts[1];

        return (user, password);
    }

    public async Task<(bool isValid, IEnumerable<Claim> claims)> ValidateAsync(string username, string password, CancellationToken ct = default)
    {
        var user = await _db.User
            .Where(u => u.UserName == username)
            .FirstOrDefaultAsync(ct);

        if (user is null)
            return (false, Array.Empty<Claim>());

        var valid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        if (!valid)
            return (false, Array.Empty<Claim>());

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserName),
            new(ClaimTypes.Name, user.UserName)
        };
        if (!string.IsNullOrWhiteSpace(user.Roles))
            claims.Add(new Claim(ClaimTypes.Role, user.Roles));

        return (true, claims);
    }
}
