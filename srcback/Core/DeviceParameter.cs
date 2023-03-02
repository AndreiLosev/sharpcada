using EnitityDeviceParametr = sharpcada.Data.Entities.DeviceParameter;
using sharpcada.Core.Enams;
using sharpcada.Core.DTO;

namespace sharpcada.Core;

public struct DeviceParameter
{
    private string _name;
    private string? _unit;
    private ParameterType _type;
    private float _castK;
    private float _castB;
    private float? _vlaue;
    private Func<ForNetworkChnel> _convertToBytes;

    public DeviceParameter(
        EnitityDeviceParametr deviceParament,
        Func<ForNetworkChnel> convertToBytes)
    {
        _name = deviceParament.Name;
        _unit = deviceParament.Unit;
        _type = deviceParament.Type;
        _castK = deviceParament.CastK;
        _castB = deviceParament.CastB;
        _convertToBytes = convertToBytes;
    }
}
