using Moq;
using Promomash.Core.Entities;
using Promomash.Core.Interfaces;
using Promomash.Core.Services;
using Promomash.Core.Specifications;
using Xunit;

namespace Promomash.UnitTests.Services.AddUserTests;

public class AddUserTest
{
    private readonly Mock<IRepository<User>> _mockUserRepo = new();


    [Fact]
    public async void InvokesUserRepository_Add_Once()
    {
        // Arrange
        var email = "test@gmail.com";
        var password = "Akt2Nono";
        var provinceId = 1;

        // Act
        var userService = new UserService(_mockUserRepo.Object);
        await userService.AddAsync(email, password, provinceId, default);

        // Assert
        _mockUserRepo.Verify(x => x.AddAsync(It.Is<User>(x => x.Email == email), default), Times.Once);
    }

    [Fact]
    public async void InvokesUserRepository_Exists_Once()
    {
        // Arrange
        var email = "test@gmail.com";

        // Act
        var userService = new UserService(_mockUserRepo.Object);
        await userService.IsExistAsync(email, default);

        // Assert
        _mockUserRepo.Verify(x => x.AnyAsync(It.IsAny<UserByNameItemsSpecification>(), default), Times.Once);
    }
}
