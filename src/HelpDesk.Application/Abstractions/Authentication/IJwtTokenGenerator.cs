using HelpDesk.Domain.Entities;

namespace HelpDesk.Application.Abstractions.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
