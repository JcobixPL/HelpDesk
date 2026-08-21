namespace HelpDesk.Domain.Entities;

public class Project
{
    public Guid Id {  get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
    public string Description { get; set; }
    public Guid CreatedById { get; set; }
    public DateTime CreatedAt { get; set; }
    public User CreatedBy { get; set; }
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    private Project()
    {
    }

    public Project(
        string name, 
        string key,
        string description,
        Guid createdById)
    {
        Id = Guid.NewGuid();
        Name = name;
        Key = key;
        Description = description;
        CreatedById = createdById;
        CreatedAt = DateTime.UtcNow;
    }
}
