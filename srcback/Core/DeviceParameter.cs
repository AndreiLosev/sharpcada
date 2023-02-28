namespace sharpcada.Core;

public struct DeviceParameter
{
    private string _name;
    private string? _unit;
    private ParameterType _type;
    private float _castK;
    private float _castB;
    private object _vlaue;

    public long Id {get; set;}
}
