
namespace TheWayToGerman.Logic.Authentication;

public class AuthConfig
{

    public required string TokenKey { get; set; }
    public int MinutesToExpire { get; set; }
}
