using Core.DataKit.MockWrapper;
using Core.DataKit.Result;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TheWayToGerman.Logic.Authentication
{
    public class JwtService : IAuthService
    {
        public AuthConfig AuthConfig { get; }
        public IDateTimeProvider DateTimeProvider { get; }

        public JwtService(AuthConfig authConfig,IDateTimeProvider dateTimeProvider)
        {
            AuthConfig = authConfig;
            DateTimeProvider = dateTimeProvider;
        }
        IEnumerable<Claim> CreateClaim(params string[] claims)
        {

            foreach (var claim in claims)
            {
                yield return new Claim(claim.GetType().Name, claim);
            }

        }
        public Result<string> GenerateToken(params string[] claimsValue)
        {
            try
            {
                if (claimsValue.Length==0)
                {
                    return new ArgumentNullException(nameof(claimsValue));
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtKey = Encoding.UTF8.GetBytes(AuthConfig.TokenKey);
                var tokenAttributes = new SecurityTokenDescriptor
                {
                    //Claims
                    Subject = new ClaimsIdentity(CreateClaim(claimsValue)),
                    //The Expiration Date Of Token
                    Expires = DateTimeProvider.UtcNow.AddMinutes(AuthConfig.MinutesToExpire),
                    //Set cryptographic key and security algorithms that are used to generate a digital signature.
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenAttributes);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Result<DecryptedToken> DecryptToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                return new DecryptedToken()
                {
                    Claims = jwtToken.Claims,
                    IsValid = jwtToken.ValidTo > DateTimeProvider.UtcNow
                };
            }
            catch (Exception ex)
            {
                return ex;
            }

        }

    }
}
