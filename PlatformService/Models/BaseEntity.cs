using System.ComponentModel.DataAnnotations;

namespace PlatformService.Models;

public abstract class BaseEntity<TPrimaryKeyType> where TPrimaryKeyType : struct
{
    [Key]
    [Required]
    public TPrimaryKeyType Id { get; set; }
}

public abstract class EntityBase : BaseEntity<int>
{
    
}