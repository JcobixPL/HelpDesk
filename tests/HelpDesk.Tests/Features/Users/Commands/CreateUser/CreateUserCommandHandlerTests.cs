using AutoMapper;
using HelpDesk.Application.Features.Users.Commands.Create;
using HelpDesk.Application.Mappings;
using HelpDesk.Domain.Abstractions.Repositories;
using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Microsoft.Extensions.Logging;

namespace HelpDesk.Tests.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateUserAndReturnUserDto()
    {
        // Arrange
        var userRepository = new Mock<IUserRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var services = new ServiceCollection();

        services.AddLogging();

        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<UserMappingProfile>();
        });

        using var serviceProvider = services.BuildServiceProvider();

        var mapper = serviceProvider.GetRequiredService<IMapper>();

        var handler = new CreateUserCommandHandler(
            userRepository.Object,
            unitOfWork.Object,
            mapper);

        var command = new CreateUserCommand(
            "Jan",
            "Kowalski",
            "jan.kowalski@test.pl",
            UserRole.Employee);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Jan", result.FirstName);
        Assert.Equal("Kowalski", result.LastName);
        Assert.Equal("jan.kowalski@test.pl", result.Email);
        Assert.Equal(UserRole.Employee, result.Role);

        userRepository.Verify(
            x => x.Add(It.IsAny<User>()),
            Times.Once);

        unitOfWork.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }
}