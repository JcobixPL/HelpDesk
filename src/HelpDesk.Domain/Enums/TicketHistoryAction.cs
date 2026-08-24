namespace HelpDesk.Domain.Enums;

public enum TicketHistoryAction
{
    Created,
    Updated,
    Assigned,
    Unassigned,
    StatusChanged,
    PriorityChanged,
    CommentAdded,
    CommentUpdated,
    CommentDeleted
}
