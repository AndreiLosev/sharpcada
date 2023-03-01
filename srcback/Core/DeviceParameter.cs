using EnitityDeviceParametr = sharpcada.Data.Entities.DeviceParameter;
using sharpcada.Core.Enams;

namespace sharpcada.Core;

public struct DeviceParameter
{
    private string _name;
    private string? _unit;
    private ParameterType _type;
    private float _castK;
    private float _castB;
    private object?  _vlaue;

    public long Id {get; set;}

    public DeviceParameter(EnitityDeviceParametr deviceParament)
    {
        Id = deviceParament.Id;
        _name = deviceParament.Name;
        _unit = deviceParament.Unit;
        _type = deviceParament.Type;
        _castK = deviceParament.CastK;
        _castB = deviceParament.CastB;
    }
}
