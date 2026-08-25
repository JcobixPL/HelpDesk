using HelpDesk.Domain.Entities;

namespace HelpDesk.Application.Abstractions.Authentication;

public interface IPasswordHasher
{
    string HashPassword(User user, string password);

    bool VerifyHashedPassword(User user, string passwordHash, string providedPassword);
}
