namespace sharpcada.Data.Entities;

public abstract class EntityBaseWhitDate : EntityBase
{
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

