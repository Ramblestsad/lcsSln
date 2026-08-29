using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using Todo.WebApi.Auth.Contracts;
using Todo.WebApi.Infrastructure.Configuration;
using Todo.WebApi.Infrastructure.Validation;

namespace Todo.WebApi.Auth;

public static class AuthEndpoints
{
    public static WebApplication MapAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/Auth")
            .WithTags("Auth");

        group.MapPost("/register", RegisterAsync)
            .AllowAnonymous()
            .WithName("RegisterUser")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapPost("/login", LoginAsync)
            .AllowAnonymous()
            .WithName("LoginUser")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        return app;
    }

    private static async Task<IResult> RegisterAsync(
        UserRegisterDetails? userData,
        UserManager<IdentityUser> userManager)
    {
        if (!RequestValidation.TryValidate(userData, out _))
        {
            return Results.BadRequest(new { Message = "User Registration Failed" });
        }

        var user = new IdentityUser { UserName = userData!.UserName, Email = userData.Email };
        var result = await userManager.CreateAsync(user, userData.Password);
        if (!result.Succeeded)
        {
            var modelState = new ModelStateDictionary();
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.Code, error.Description);
            }

            return Results.BadRequest(new
            {
                Message = "User Registration Failed",
                Erros = modelState
            });
        }

        return Results.Ok(new { Message = "User Registration Successful" });
    }

    private static async Task<IResult> LoginAsync(
        UserLoginCredentials? userData,
        UserManager<IdentityUser> userManager,
        IConfiguration configuration)
    {
        if (!RequestValidation.TryValidate(userData, out _))
        {
            return Results.BadRequest(new { Message = "Login failed" });
        }

        var identityUser = await ValidateUserAsync(userManager, userData!);
        if (identityUser is null)
        {
            return Results.BadRequest(new { Message = "Login failed" });
        }

        var token = GenerateToken(configuration, identityUser);
        return Results.Ok(new { Token = token, Message = "Success" });
    }

    private static async Task<IdentityUser?> ValidateUserAsync(
        UserManager<IdentityUser> userManager,
        UserLoginCredentials userData)
    {
        var identityUser = await userManager.FindByNameAsync(userData.UserName);
        if (identityUser is null || identityUser.PasswordHash is null)
        {
            return null;
        }

        var result = userManager.PasswordHasher.VerifyHashedPassword(
            identityUser,
            identityUser.PasswordHash,
            userData.Password);

        return result == PasswordVerificationResult.Failed ? null : identityUser;
    }

    private static string? GenerateToken(IConfiguration configuration, IdentityUser identityUser)
    {
        var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>()!;
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtSettings.Key!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Sub, identityUser.Id),
                new Claim(ClaimTypes.NameIdentifier, identityUser.Id),
                new Claim(ClaimTypes.Name, identityUser.UserName!),
                new Claim(ClaimTypes.Email, identityUser.Email!)
            ]),
            Expires = DateTime.UtcNow.AddDays(15),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = jwtSettings.Audience,
            Issuer = jwtSettings.Issuer
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
