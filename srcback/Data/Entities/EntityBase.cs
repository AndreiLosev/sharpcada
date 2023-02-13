namespace sharpcada.Data.Entities;

public abstract class EntityBase
{
    public ulong Id { set; get; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
