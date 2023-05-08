
using Core.Cqrs.Requests;
using Core.DataKit.MockWrapper;
using Core.DataKit.Result;
using MediatR;
using Moq;
using TheWayToGerman.Core.Cqrs.Queries;
using TheWayToGerman.Logic.Authentication;

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
