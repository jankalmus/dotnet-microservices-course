using System.ComponentModel.DataAnnotations;

namespace CommandService.DTO;

public class CommandCreateDto
{
    [Required]
    public string HowTo { get; set; } = default!; 
    
    [Required]
    public string CommandLine { get; set; } = default!;
}