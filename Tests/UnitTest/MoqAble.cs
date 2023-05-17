using Core.DataKit.MockWrapper;
using Moq;

namespace UnitTest;

public static class MoqAble
{
    public static IMock<IDateTimeProvider> CreateDateTimeProvider(DateTime now, DateTime ytcNow)
    {
        var dateTimeProviderMoq = new Mock<IDateTimeProvider>();
        dateTimeProviderMoq.Setup(x => x.Now).Returns(now);
        dateTimeProviderMoq.Setup(x => x.UtcNow).Returns(ytcNow);
        return dateTimeProviderMoq;
    }

}
