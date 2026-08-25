using HelpDesk.Application.DTOs.Tickets;
using MediatR;

namespace HelpDesk.Application.Features.Tickets.Commands.Unassign;

public record UnassignTicketCommand(Guid TicketId) : IRequest<TicketDto>;