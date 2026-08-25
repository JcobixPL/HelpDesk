using HelpDesk.Domain.Enums;

namespace HelpDesk.Application.DTOs.Tickets;

public record TicketDto(
    Guid Id,
    string Key,
    string Title,
    string Description,
    TicketStatus Status,
    TicketPriority Priority,
    TicketType Type,
    Guid ProjectId,
    Guid ReporterId,
    Guid? AssigneeId,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    DateTime? ResolvedAt);