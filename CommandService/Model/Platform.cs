namespace CommandService.Model;

public class Platform : EntityBase
{
    public int ExternalId { get; set; }

    public string Name { get; set; } = default!; 

    public ICollection<Command> Commands { get; set; } = new List<Command>(); 
}