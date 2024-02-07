using System.ComponentModel.DataAnnotations;

namespace Todo.DAL.Entity;
/// <summary>
/// User login credentials.
/// </summary>
public class UserLoginCredentials
{
    /// <summary>
    /// User name.
    /// </summary>
    [Required] public string UserName { get; set; } = null!;
    /// <summary>
    /// User password.
    /// </summary>
    [Required] public string Password { get; set; } = null!;
    /// <summary>
    /// User email.
    /// </summary>
    [Required] public string Email { get; set; } = null!;
}

/// <summary>
/// User login credentials.
/// </summary>
public class UserRegisterDetails
{
    /// <summary>
    /// User name.
    /// </summary>
    [Required] public string UserName { get; set; } = null!;
    /// <summary>
    /// User password.
    /// </summary>
    [Required] public string Password { get; set; } = null!;
    /// <summary>
    /// User email.
    /// </summary>
    [Required] public string Email { get; set; } = null!;
}
