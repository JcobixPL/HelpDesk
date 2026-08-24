namespace HelpDesk.Application.DTOs.Users;

public record UpdateUserRequest(
    string FirstName,
    string LastName);