using HelpDesk.Application.DTOs.Tickets;
using HelpDesk.Domain.Enums;
using MediatR;

namespace HelpDesk.Application.Features.Tickets.Commands.ChangeStatus;

public record ChangeTicketStatusCommand(
    Guid TicketId,
    TicketStatus Status) : IRequest<TicketDto>;