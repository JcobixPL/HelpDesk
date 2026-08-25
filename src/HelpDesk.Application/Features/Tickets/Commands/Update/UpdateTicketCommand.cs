using HelpDesk.Application.DTOs.Tickets;
using HelpDesk.Domain.Enums;
using MediatR;

namespace HelpDesk.Application.Features.Tickets.Commands.Update;

public record UpdateTicketCommand(
    Guid Id,
    string Title,
    string Description,
    TicketPriority Priority,
    TicketType Type) : IRequest<TicketDto>;
