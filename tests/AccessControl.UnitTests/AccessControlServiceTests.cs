using AccessControl.API.Grpc;
using AccessControl.Entities.Models;
using AccessControl.Infrastructure;
using AccessControl.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Moq.EntityFrameworkCore;

namespace AccessControl.UnitTests;

public sealed class AccessControlServiceTests
{
    [Fact]
    public async Task VerifyAccessKeyReturnsSuccessWithUserId()
    {
        // Arrange
        const string userId = "123";
        const string id = "abc";
        const string secretKey = "321";

        AccessKey accessKey = new(userId, id, secretKey);
        accessKey.SetActivityStatus(true);
        var data = new List<AccessKey> { accessKey };

        var mockContext = new Mock<AccessControlContext>();
        mockContext.Setup<DbSet<AccessKey>>(c => c.AccessKeys)
            .ReturnsDbSet(data);
        
        var service = new AccessControlService(mockContext.Object, NullLogger<AccessControlService>.Instance);
        var serverCallContext = TestServerCallContext.Create();
        serverCallContext.SetUserState("__HttpContext", new DefaultHttpContext());
        
        // Act
        var response = await service.VerifyAccessKey(new VerificationRequest { SecretKey = secretKey}, serverCallContext);
        
        // Assert
        response.Should().BeOfType<VerificationResponse>();
        response.UserId.Should().Be(userId);
        response.Verified.Should().BeTrue();
    }
}