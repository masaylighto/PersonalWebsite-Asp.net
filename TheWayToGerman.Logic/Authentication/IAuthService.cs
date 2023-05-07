
using Core.DataKit.Result;

namespace TheWayToGerman.Logic.Authentication;

public interface IAuthService
{
    Result<string> GenerateToken(params string[] claimsValue);
    Result<DecryptedToken> DecryptToken(string token);
}
