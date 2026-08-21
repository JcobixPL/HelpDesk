using HelpDesk.Domain.Enums;

namespace HelpDesk.Domain.Entities;

public class Ticket
{
    public Guid Id { get; set; }
    public string Key { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TicketStatus Status { get; set; }
    public TicketPriority Priority { get; set; }
    public TicketType Type { get; set; }
    public Guid ProjectId { get; set; }
    public Guid ReporterId { get; set; }
    public Guid? AssigneeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }
    public Project Project { get; set; }
    public User Reporter { get; set; }
    public User? Assignee { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    private Ticket()
    {
    }

    public Ticket(
        string key,
        string title,
        string description,
        TicketPriority priority,
        TicketType type,
        Guid projectId,
        Guid reporterId
        )
    {
        Id = Guid.NewGuid();
        Key = key;
        Title = title;
        Description = description;
        Priority = priority;
        Type = type;
        ProjectId = projectId;
        ReporterId = reporterId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        Status = TicketStatus.Open;
    }

    public void Update(
        string title,
        string description,
        TicketPriority priority,
        TicketType type)
    {
        Title = title;
        Description = description;
        Priority = priority;
        Type = type;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AssignTo(Guid userId)
    {
        AssigneeId = userId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Unassign()
    {
        AssigneeId = null;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangePriority(TicketPriority priority)
    {
        Priority = priority;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeStatus(TicketStatus newStatus)
    {
        if (!CanChangeStatus(newStatus))
        {
            throw new InvalidOperationException(
                $"Cannot change ticket status from {Status} to {newStatus}.");
        }

        Status = newStatus;

        if (newStatus == TicketStatus.Resolved)
        {
            ResolvedAt = DateTime.UtcNow;
        }

        UpdatedAt = DateTime.UtcNow;
    }

    private bool CanChangeStatus(TicketStatus newStatus)
    {
        return (Status, newStatus) switch
        {
            (TicketStatus.Open, TicketStatus.InProgress) => true,
            (TicketStatus.InProgress, TicketStatus.Resolved) => true,
            (TicketStatus.Resolved, TicketStatus.Closed) => true,
            (TicketStatus.Resolved, TicketStatus.InProgress) => true,
            _ => false
        };
    }
}
