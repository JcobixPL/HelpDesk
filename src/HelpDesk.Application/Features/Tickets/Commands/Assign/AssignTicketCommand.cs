using HelpDesk.Application.DTOs.Tickets;
using MediatR;

namespace HelpDesk.Application.Features.Tickets.Commands.Assign;

public record AssignTicketCommand(
    Guid TicketId,
    Guid UserId) : IRequest<TicketDto>;
