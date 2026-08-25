using HelpDesk.Domain.Enums;

namespace HelpDesk.Application.DTOs.Tickets;

public record UpdateTicketRequest(
    string Title,
    string Description,
    TicketPriority Priority,
    TicketType Type);
