
using Core.DataKit.Result;

namespace TheWayToGerman.Logic.Authentication;

public interface IAuthService
{
    Result<string> GenerateToken(params (string name, string value)[] claimsValue);
    Result<DecryptedToken> DecryptToken(string token);
}
