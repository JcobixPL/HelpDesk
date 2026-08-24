using HelpDesk.Application.Features.Users.Commands.Create;
using HelpDesk.Domain.Enums;

namespace HelpDesk.Tests.Features.Users.Commands.CreateUser;

public class CreateUserValidatorTests
{
    private readonly CreateUserValidator _validator = new();

    [Fact]
    public async Task Validate_ShouldPass_WhenCommandIsValid()
    {
        // Arrange
        var command = new CreateUserCommand(
            "Jan",
            "Kowalski",
            "jan.kowalski@test.pl",
            UserRole.Employee);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_ShouldFail_WhenFirstNameIsEmpty()
    {
        var command = new CreateUserCommand(
            "",
            "Kowalski",
            "jan.kowalski@test.pl",
            UserRole.Employee);

        var result = await _validator.ValidateAsync(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, x => x.PropertyName == nameof(command.FirstName));
    }

    [Fact]
    public async Task Validate_ShouldFail_WhenLastNameIsEmpty()
    {
        var command = new CreateUserCommand(
            "Jan",
            "",
            "jan.kowalski@test.pl",
            UserRole.Employee);

        var result = await _validator.ValidateAsync(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, x => x.PropertyName == nameof(command.LastName));
    }

    [Fact]
    public async Task Validate_ShouldFail_WhenEmailIsEmpty()
    {
        var command = new CreateUserCommand(
            "Jan",
            "Kowalski",
            "",
            UserRole.Employee);

        var result = await _validator.ValidateAsync(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, x => x.PropertyName == nameof(command.Email));
    }

    [Fact]
    public async Task Validate_ShouldFail_WhenRoleIsInvalid()
    {
        var command = new CreateUserCommand(
            "Jan",
            "Kowalski",
            "jan.kowalski@test.pl",
            (UserRole)999);

        var result = await _validator.ValidateAsync(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, x => x.PropertyName == nameof(command.Role));
    }
}