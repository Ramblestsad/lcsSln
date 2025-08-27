using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using Todo.DAL.Context;
using Todo.DAL.Dto;
using Todo.WebApi.Configuration;

namespace Todo.WebApi.Controllers;

/// <summary>
/// Authentication controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase {
    private readonly IConfiguration _configuration;
    private readonly ApplicationIdentityDbContext _context;
    private readonly ILogger<AuthController> _logger;
    private readonly UserManager<IdentityUser> _userManager;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="config"></param>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    public AuthController(
        UserManager<IdentityUser> userManager,
        IConfiguration config,
        ApplicationIdentityDbContext context,
        ILogger<AuthController> logger) {
        _configuration = config;
        _context = context;
        _logger = logger;
        _userManager = userManager;
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="userData"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDetails? userData) {
        if (!ModelState.IsValid || userData == null) {
            return new BadRequestObjectResult(new { Message = "User Registration Failed" });
        }

        var user = new IdentityUser() { UserName = userData.UserName, Email = userData.Email };
        var res = await _userManager.CreateAsync(user, userData.Password);
        if (!res.Succeeded) {
            var dict = new ModelStateDictionary();
            foreach (var err in res.Errors) {
                dict.AddModelError(err.Code, err.Description);
            }

            return new BadRequestObjectResult(new {
                Message = "User Registration Failed",
                Erros =
                    dict
            });
        }

        return Ok(new { Message = "User Registration Successful" });
    }

    /// <summary>
    /// Login a user
    /// </summary>
    /// <param name="userData"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginCredentials? userData) {
        IdentityUser? identityUser;
        if (!ModelState.IsValid
            || userData == null
            || ( identityUser = await ValidateUser(userData) ) == null) {
            return new BadRequestObjectResult(new { Message = "Login failed" });
        }

        var token = GenerateToken(identityUser);

        return Ok(new { Token = token, Message = "Success" });
    }

    private async Task<IdentityUser?> ValidateUser(UserLoginCredentials? userData) {
        var identityUser = await _userManager.FindByNameAsync(userData?.UserName!);
        if (identityUser == null) return null;

        var result = _userManager.PasswordHasher.VerifyHashedPassword(
            identityUser,
            identityUser.PasswordHash!,
            userData?.Password!
        );

        return result == PasswordVerificationResult.Failed ? null : identityUser;
    }

    private string? GenerateToken(IdentityUser identityUser) {
        var jwtSettings = _configuration.GetSection("Jwt").Get<JwtSettings>()!;
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtSettings.Key!);

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name, identityUser.UserName!), new Claim(ClaimTypes.Email, identityUser.Email!)
            }),
            Expires = DateTime.UtcNow.AddDays(15),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
            ),
            Audience = jwtSettings.Audience,
            Issuer = jwtSettings.Issuer
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
