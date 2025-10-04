using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Providers;

namespace Configs;

public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IUserAuthValidator _userAuthValidator;
    public BasicAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IUserAuthValidator userAuthValidator
    ) : base(options, logger, encoder)
    {
        _userAuthValidator = userAuthValidator;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.Fail("Missing Auth");

        try
        {
            var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

            if (!header.ToString().StartsWith("Basic"))
                return AuthenticateResult.Fail("Not a basic auth token");

            var token = header.ToString();

            var (user, password) = _userAuthValidator.ParseBasicToken(token);

            var (isValid, claims) = await _userAuthValidator.ValidateAsync(user, password);

            if (!isValid)
                return AuthenticateResult.Fail("Invalid Username/Password");

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);

        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail($"Authentication Failed: {ex.Message}");
        }
    }
}