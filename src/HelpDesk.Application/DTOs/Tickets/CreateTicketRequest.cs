using HelpDesk.Domain.Enums;

namespace HelpDesk.Application.DTOs.Tickets;

public record CreateTicketRequest(
    string Title,
    string Description,
    TicketPriority Priority,
    TicketType Type,
    Guid ProjectId);