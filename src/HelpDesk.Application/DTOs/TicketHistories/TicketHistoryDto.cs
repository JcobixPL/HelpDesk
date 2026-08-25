using HelpDesk.Domain.Enums;

namespace HelpDesk.Application.DTOs.TicketHistories;

public record TicketHistoryDto(
    Guid Id,
    Guid TicketId,
    Guid UserId,
    TicketHistoryAction Action,
    string? OldValue,
    string? NewValue,
    DateTime CreatedAt);
