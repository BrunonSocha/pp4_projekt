using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Moq;
using UserService;
using UserService.Repositories;

namespace UserService.Test
{
    public class LoginServiceTests
    {
        [Fact]
        public async Task Login_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var username = "testuser";
            var password = "password123";
            var user = new EShopAbstractions.Models.User
            {
                UserId = Guid.NewGuid(),
                Username = username,
                Password = password,
                Group = "Client"
            };

            var mockRepo = new Mock<IRepository>();
            var mockJwt = new Mock<IJwtTokenService>();

            mockRepo.Setup(r => r.GetByUsernameAsync(username)).ReturnsAsync(user);
            mockJwt.Setup(j => j.GenerateToken(user.UserId, user.Group)).Returns("mocked-jwt-token");

            var loginService = new LoginService(mockJwt.Object, mockRepo.Object);

            // Act
            var token = await loginService.Login(username, password);

            // Assert
            Assert.Equal("mocked-jwt-token", token);
        }

        [Fact]
        public async Task Login_InvalidPassword_ThrowsException()
        {
            var username = "testuser";
            var user = new EShopAbstractions.Models.User
            {
                UserId = Guid.NewGuid(),
                Username = username,
                Password = "correct-password",
                Group = "Client"
            };

            var mockRepo = new Mock<IRepository>();
            var mockJwt = new Mock<IJwtTokenService>();

            mockRepo.Setup(r => r.GetByUsernameAsync(username)).ReturnsAsync(user);

            var loginService = new LoginService(mockJwt.Object, mockRepo.Object);

            await Assert.ThrowsAsync<UserService.InvalidCredentialsException>(() =>
                loginService.Login(username, "wrong-password"));
        }

        [Fact]
        public async Task Login_UnknownUsername_ThrowsException()
        {
            var mockRepo = new Mock<IRepository>();
            var mockJwt = new Mock<IJwtTokenService>();

            mockRepo.Setup(r => r.GetByUsernameAsync("notfound")).ReturnsAsync((EShopAbstractions.Models.User)null!);

            var loginService = new LoginService(mockJwt.Object, mockRepo.Object);

            await Assert.ThrowsAsync<InvalidCredentialsException>(() =>
                loginService.Login("notfound", "password"));
        }
    }
}
