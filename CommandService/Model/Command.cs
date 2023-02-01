using System.ComponentModel.DataAnnotations;

namespace CommandService.Model;

public class Command : EntityBase
{
    [Required] 
    public string HowTo { get; set; } = default!; 

    [Required] 
    public string CommandLine { get; set; } = default!;

    [Required]
    public int PlatformId { get; set; }

    public Platform Platform { get; set; } = default!; 
}