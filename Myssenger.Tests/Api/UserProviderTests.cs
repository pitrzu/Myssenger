using Microsoft.AspNetCore.Http;
using Moq;

namespace Myssenger.Tests.Api;

public sealed class UserProviderTests
{
    private Mock<IHttpContextAccessor> _mock;

    [SetUp]
    public void SetUp()
    {
        _mock = new Mock<IHttpContextAccessor>();
        _mock.SetupGet(provider => provider.HttpContext).Returns(new DefaultHttpContext());
    }
}