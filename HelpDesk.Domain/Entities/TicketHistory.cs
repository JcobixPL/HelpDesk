using HelpDesk.Domain.Enums;

namespace HelpDesk.Domain.Entities;

public class TicketHistory
{
    public Guid Id { get; set; }
    public Guid TicketId { get; set; }
    public Guid UserId { get; set; }
    public TicketHistoryAction Action { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public DateTime CreatedAt { get; set; }
    public Ticket Ticket { get; set; } = null!;
    public User User { get; set; } = null!;

    private TicketHistory()
    {
    }

    public TicketHistory(
        Guid ticketId,
        Guid userId,
        TicketHistoryAction action,
        string? oldValue = null,
        string? newValue = null)
    {
        Id = Guid.NewGuid();
        TicketId = ticketId;
        UserId = userId;
        Action = action;
        OldValue = oldValue;
        NewValue = newValue;
        CreatedAt = DateTime.UtcNow;
    }
}
