using System.Threading.Tasks;
using Xunit;
using Moq;
using UserService;
using UserService.Models;
using UserService.Repositories;
using EShopAbstractions;
using EShopAbstractions.Models;
namespace UserService.Test
{
    public class RegisterServiceTests
    {
        [Fact]
        public async Task Register_NewUser_ReturnsTrue()
        {
            // Arrange
            var request = new RegisterRequest
            {
                Username = "testuser",
                Email = "test@example.com",
                Password = "password123"
            };

            var mockContext = new Mock<IEShopDbContext>();
            var mockRepo = new Mock<IRepository>();
            var mockJwt = new Mock<IJwtTokenService>();

            mockRepo.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((EShopAbstractions.Models.User)null!);
            mockRepo.Setup(r => r.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync((EShopAbstractions.Models.User)null!);
            mockRepo.Setup(r => r.AddAsync(It.IsAny<EShopAbstractions.Models.User>())).ReturnsAsync(new EShopAbstractions.Models.User());

            var service = new RegisterService(mockContext.Object, mockRepo.Object, mockJwt.Object);

            // Act
            var result = await service.Register(request);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Register_ExistingUsername_ThrowsException()
        {
            // Arrange
            var request = new RegisterRequest
            {
                Username = "existinguser",
                Email = "existing@example.com",
                Password = "password123"
            };

            var mockContext = new Mock<IEShopDbContext>();
            var mockRepo = new Mock<IRepository>();
            var mockJwt = new Mock<IJwtTokenService>();

            mockRepo.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((EShopAbstractions.Models.User)null!);
            mockRepo.Setup(r => r.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(new EShopAbstractions.Models.User());

            var service = new RegisterService(mockContext.Object, mockRepo.Object, mockJwt.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.Register(request));
        }
    }
}
