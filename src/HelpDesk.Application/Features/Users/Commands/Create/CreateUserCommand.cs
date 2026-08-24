using HelpDesk.Application.DTOs.Users;
using HelpDesk.Domain.Enums;
using MediatR;

namespace HelpDesk.Application.Features.Users.Commands.Create;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    UserRole Role) : IRequest<UserDto>;
