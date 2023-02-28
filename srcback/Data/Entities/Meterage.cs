namespace sharpcada.Data.Entities;

public class Meterage : EntityBase
{
    public DateTime CreatedAt { get; set; }
    public string Value { get; set; } = "";
    public long DeviceParameterID { set; get; }
}

