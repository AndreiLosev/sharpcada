namespace sharpcada.Data.Entities;

public class Meterage
{
    public long Id { set; get; }
    public DateTime CreatedAt { get; set; }
    public string Value { get; set; } = "";
    public long DeviceParameterID { set; get; }
}

