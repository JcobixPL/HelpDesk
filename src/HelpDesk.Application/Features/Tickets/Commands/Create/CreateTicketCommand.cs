using HelpDesk.Application.DTOs.Tickets;
using HelpDesk.Domain.Enums;
using MediatR;

namespace HelpDesk.Application.Features.Tickets.Commands.Create;

public record CreateTicketCommand(
    string Title,
    string Description,
    TicketPriority Priority,
    TicketType Type,
    Guid ProjectId,
    Guid ReporterId) : IRequest<TicketDto>;
