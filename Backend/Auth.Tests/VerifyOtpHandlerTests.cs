using Auth.Application.Features.Otp.Verify;
using Auth.Application.Interfaces;
using Auth.Domain.Entities;
using Moq;
using StackExchange.Redis;

namespace Auth.Tests;

public class VerifyOtpHandlerTests
{
    private readonly Mock<IConnectionMultiplexer> _cacheMock = new();
    private readonly Mock<IUserRepository> _userRepoMock = new();

    [Fact]
    public async Task Handle_ValidCode_ReturnsTrue()
    {
        var cmd = new VerifyOtpCommand("test@test.com", "123456");
        
        _cacheMock.Setup(c => c.GetDatabase().StringGetAsync("otp:test@test.com")).ReturnsAsync("123456");
        _userRepoMock.Setup(r => r.GetByEmailAsync("test@test.com")).ReturnsAsync(new User());

        var handler = new VerifyOtpHandler(_cacheMock.Object, _userRepoMock.Object);
        
        var result = await handler.Handle(cmd, CancellationToken.None);
        
        Assert.True(result);
        _cacheMock.Verify(c => c.GetDatabase().KeyDeleteAsync("otp:test@test.com"), Times.Once);
        _userRepoMock.Verify(r => r.SetVerifiedAsync(It.IsAny<User>(), true), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidCode_ReturnsFalse()
    {
        var cmd = new VerifyOtpCommand("test@test.com", "wrong");
        _cacheMock.Setup(c => c.GetDatabase().StringGetAsync("otp:test@test.com")).ReturnsAsync("123456");
    
        var handler = new VerifyOtpHandler(_cacheMock.Object, _userRepoMock.Object);
        
        var result = await handler.Handle(cmd, CancellationToken.None);
        
        Assert.False(result);
        _userRepoMock.Verify(r => r.SetVerifiedAsync(It.IsAny<User>(), true), Times.Never);
    }
}