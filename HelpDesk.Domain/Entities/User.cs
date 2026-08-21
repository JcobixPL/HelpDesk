using HelpDesk.Domain.Enums;

namespace HelpDesk.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }

    private User()
    { 
    }

    public User(
        string firstName,
        string lastName,
        string email,
        UserRole role)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Role = role;
        CreatedAt = DateTime.UtcNow;
    }
}
