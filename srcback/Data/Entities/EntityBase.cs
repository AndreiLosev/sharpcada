using System.ComponentModel.DataAnnotations;

namespace sharpcada.Data.Entities;

public abstract class EntityBase
{
    [Key]
    public long Id { init; get; }
}
