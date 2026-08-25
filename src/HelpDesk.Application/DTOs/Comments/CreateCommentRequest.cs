namespace HelpDesk.Application.DTOs.Comments;

public record CreateCommentRequest(
    string Content,
    Guid AuthorId);