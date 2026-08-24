using HelpDesk.Application.DTOs.Users;
using MediatR;

namespace HelpDesk.Application.Features.Users.Commands.Update;

public record UpdateUserCommand(
    Guid Id,
    string FirstName,
    string LastName) : IRequest<UserDto>;

