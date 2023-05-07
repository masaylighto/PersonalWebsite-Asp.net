

using System.Security.Claims;

namespace TheWayToGerman.Logic.Authentication;

public class DecryptedToken
{
    public IEnumerable<Claim>? Claims { get; set; }
    public bool IsValid { get; set; }
}
