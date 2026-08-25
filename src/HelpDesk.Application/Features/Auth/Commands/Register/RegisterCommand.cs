using HelpDesk.Application.DTOs.Users;
using MediatR;

namespace HelpDesk.Application.Features.Auth.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<UserDto>;

