namespace HelpDesk.Domain.Entities;

public class Project
{
    public Guid Id {  get; set; }
    public string Name { get; set; } = null!;
    public string Key { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int TicketSequence { get; set; } 
    public Guid CreatedById { get; set; }
    public DateTime CreatedAt { get; set; }
    public User CreatedBy { get; set; } = null!;
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

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public int GetNextTicketNumber()
    {
        TicketSequence++;
        return TicketSequence;
    }
}
