namespace HelpDesk.Application.DTOs.Projects;

public record ProjectDto(
    Guid Id,
    string Name, 
    string Key,
    string Description,
    Guid CreatedById,
    DateTime CreatedAt);

