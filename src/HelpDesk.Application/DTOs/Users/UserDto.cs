using HelpDesk.Domain.Enums;

namespace HelpDesk.Application.DTOs.Users;

public record UserDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    UserRole Role,
    bool IsActive,
    DateTime CreatedAt);
