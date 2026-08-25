using HelpDesk.Domain.Enums;

namespace HelpDesk.Application.DTOs.Auth;

public record AuthResponseDto(
    Guid UserId,
    string Email,
    UserRole Role,
    string Token);