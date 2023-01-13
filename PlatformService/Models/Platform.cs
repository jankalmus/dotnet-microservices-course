using System.ComponentModel.DataAnnotations;

namespace PlatformService.Models;

public class Platform : EntityBase
{
    [Required] 
    public string Name { get; set; } = default!;

    [Required] 
    public string Publisher { get; set; } = default!;

    [Required]
    public string Cost { get; set; } = default!;
}