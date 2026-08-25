using HelpDesk.Application.DTOs.Tickets;
using HelpDesk.Domain.Enums;
using MediatR;

namespace HelpDesk.Application.Features.Tickets.Commands.ChangePriority;

public record ChangeTicketPriorityCommand(
    Guid TicketId,
    TicketPriority Priority) : IRequest<TicketDto>;