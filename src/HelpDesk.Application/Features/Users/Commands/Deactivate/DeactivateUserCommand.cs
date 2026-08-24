using MediatR;

namespace HelpDesk.Application.Features.Users.Commands.Deactivate;

public record DeactivateUserCommand(Guid Id) : IRequest;
