using HelpDesk.Application.DTOs.Auth;
using MediatR;

namespace HelpDesk.Application.Features.Auth.Commands.Login;

public record LoginCommand(
    string Email,
    string Password) : IRequest<AuthResponseDto>;