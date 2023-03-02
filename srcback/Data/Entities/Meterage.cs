namespace sharpcada.Data.Entities;

public class Meterage : EntityBase
{
    public DateTime CreatedAt { get; set; }
    public float Value { get; set; }
    public long DeviceParameterID { set; get; }
}

