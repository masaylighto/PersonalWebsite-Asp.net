

using Core.DataKit.MockWrapper;
using TheWayToGerman.Logic.Authentication;
using Moq;
using Bogus;

namespace UnitTest.Logic.Authentication;

public class JWTServiceTest
{
    readonly Faker FakeData = new Faker();
    readonly IAuthService JwtService;
    readonly IMock<IDateTimeProvider> DateTimeMoq = MoqAble.CreateDateTimeProvider(DateTime.Now,DateTime.UtcNow);
    public JWTServiceTest()
    {
        JwtService = CreateJwtService(DateTimeMoq);
    }
    JwtService CreateJwtService(IMock<IDateTimeProvider> DateTimeMoq)
    {
        return new JwtService(new AuthConfig()
        {
            MinutesToExpire = 15,
            SigningKey = "sadfsfsdgdfhfgjkl",
            Audience= "SomeAudience",
            Issuer= "SomeIssuer"
        }, DateTimeMoq.Object);
    }
    [Fact]
    public void GenerateToken_NoClaims_ReturnArgumentNullException()
    {
        //execute
        var result=  JwtService.GenerateToken();
        //validate        
        Assert.True(result.IsErrorOfType<ArgumentNullException>());
    }
    [Fact]
    public void GenerateToken_ClaimsExist_ReturnStringToken()
    {
        //Prepare
        string username = FakeData.Internet.UserName();
        //execute
        var result = JwtService.GenerateToken((nameof(username),username));
        //validate        
        Assert.True(result.ContainData());
    }
    [Fact]
    public void CheckIfTokenIsValid_BeforeExpire_ReturnTrue()
    {
        //Prepare
        string username = FakeData.Internet.UserName();
        var tokenResult = JwtService.GenerateToken((nameof(username), username));

        //execute
        var decryptResult = JwtService.DecryptToken(tokenResult.GetData());
        
        //validate
        Assert.True(decryptResult.ContainData());
        Assert.True(decryptResult.GetData().IsValid);
    }
    [Fact]
    public void CheckIfTokenIsValid_AfterExpire_ReturnFalse()
    {
        //Prepare
        string username = FakeData.Internet.UserName();
        var tokenResult = JwtService.GenerateToken((nameof(username), username));
        var JwtServiceExpiredDate = CreateJwtService(MoqAble.CreateDateTimeProvider(DateTime.Now.AddMinutes(16), DateTime.UtcNow.AddMinutes(16))); // to set a fake time 
        //execute
        var decryptResult = JwtServiceExpiredDate.DecryptToken(tokenResult.GetData());
       
        //validate
        Assert.True(decryptResult.ContainData());
        Assert.False(decryptResult.GetData().IsValid);
    }
    [Fact]
    public void CheckIfTokenContainClaims_CheckAddedClaim_ReturnTrue()
    {
        //Prepare
        string username = FakeData.Internet.UserName();
        var tokenResult = JwtService.GenerateToken((nameof(username), username));

        //execute
        var decryptResult = JwtService.DecryptToken(tokenResult.GetData());

        //validate
        Assert.True(decryptResult.ContainData());
        Assert.Equal(decryptResult.GetData()!.Claims!.FirstOrDefault()!.Value,username);
    }
    [Fact]
    public void CheckIfTokenContainClaims_CheckNotAddedClaim_ReturnFalse()
    {
        //Prepare
        string username = FakeData.Internet.UserName();
        var tokenResult = JwtService.GenerateToken((nameof(username), username));

        //execute
        var decryptResult = JwtService.DecryptToken(tokenResult.GetData());

        //validate
        Assert.True(decryptResult.ContainData());
        Assert.NotEqual(decryptResult!.GetData()!.Claims!.FirstOrDefault()!.Value, FakeData.Internet.UserName());
    }
}
