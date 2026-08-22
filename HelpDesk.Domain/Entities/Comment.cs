namespace HelpDesk.Domain.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public string Content { get; set; } = null!;
    public Guid TicketId { get; set; }
    public Guid AuthorId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Ticket Ticket { get; set; } = null!;
    public User Author { get; set; } = null!;

    private Comment()
    {
    }

    public Comment(
        string content,
        Guid ticketId,
        Guid authorId)
    {
        Id = Guid.NewGuid();
        Content = content;
        TicketId = ticketId;
        AuthorId = authorId;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string content)
    {
        Content = content;
        UpdatedAt = DateTime.UtcNow;
    }
}
