namespace HelpDesk.Application.DTOs.Comments;

public record CommentDto(
    Guid Id,
    string Content, 
    Guid TicketId,
    Guid AuthorId,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
