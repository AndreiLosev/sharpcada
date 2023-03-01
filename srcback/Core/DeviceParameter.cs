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
    private object? _vlaue;
    private Func<byte[], object> _convertToBytes;

    public long Id {get; set;}

    public DeviceParameter(
        EnitityDeviceParametr deviceParament,
        Func<byte[], object> convertToBytes)
    {
        Id = deviceParament.Id;
        _name = deviceParament.Name;
        _unit = deviceParament.Unit;
        _type = deviceParament.Type;
        _castK = deviceParament.CastK;
        _castB = deviceParament.CastB;
        _convertToBytes = convertToBytes;
    }
}
