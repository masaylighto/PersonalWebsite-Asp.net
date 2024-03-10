
namespace PersonalWebsiteApi.Logic.Authentication;

public class AuthConfig
{
    public required string SigningKey { get; set; }
    public required int MinutesToExpire { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
}
