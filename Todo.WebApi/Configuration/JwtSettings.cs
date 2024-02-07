namespace Todo.WebApi.Configuration;
/// <summary>
/// Jwt settings options
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Key
    /// </summary>
    public string? Key { get; set; }
    /// <summary>
    /// Issuer
    /// </summary>
    public string? Issuer { get; set; }
    /// <summary>
    /// Audience
    /// </summary>
    public string? Audience { get; set; }
    /// <summary>
    /// Subject
    /// </summary>
    public string? Subject { get; set; }
}
