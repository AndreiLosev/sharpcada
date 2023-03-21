using EnitityDeviceParametr = sharpcada.Data.Entities.DeviceParameter;
using sharpcada.Exception;
using sharpcada.Core.Enams;
using sharpcada.Core.DTO;
using sharpcada.Core.Helpers;

namespace sharpcada.Core;

public struct DeviceParameter
{
    private string _name;
    private string? _unit;
    private ParameterType _type;
    private float _castK;
    private float _castB;
    private float? _vlaue;
    private Func<float, ForNetworkChunnel> _convertToBytes;

    public DeviceParameter(
        EnitityDeviceParametr deviceParament,
        Func<float, ForNetworkChunnel> convertToBytes)
    {
        _name = deviceParament.Name;
        _unit = deviceParament.Unit;
        _type = deviceParament.Type;
        _castK = deviceParament.CastK;
        _castB = deviceParament.CastB;
        _convertToBytes = convertToBytes;
    }

    public void SetValue(byte[] value)
    {
        var predVar = _type switch
        {
            ParameterType.Bool => value.First().GetBit(0) ? 1 : 0,
            ParameterType.Uint8 => (float)value.First(),
            ParameterType.Int8 => (float)value.First(),
            ParameterType.Uint16 => (float)BitConverter.ToUInt16(value),
            ParameterType.Int16 => (float)BitConverter.ToInt16(value),
            ParameterType.Uint32 => (float)BitConverter.ToUInt32(value),
            ParameterType.Int32 => (float)BitConverter.ToInt32(value),
            ParameterType.Uint64 => (float)BitConverter.ToUInt64(value),
            ParameterType.Int64 => (float)BitConverter.ToInt64(value),
            ParameterType.Float32 => BitConverter.ToSingle(value),
            ParameterType.Float64 => (float)BitConverter.ToDouble(value),
            _ => throw new UnimplementedExceprion(),
        };

        _vlaue = _cast(predVar);
    }

    private float _cast(float value)
    {
        if (_type is ParameterType.Bool)
        {
            if (_castK < 0)
            {
                return Math.Abs(value - 1);
            }

            return value;
        }

        return _castK * value + _castB;
    }

    public DTO.DeviceParameterView GetView() =>
        new DeviceParameterView
        {
            Name = _name,
            Unit = _unit,
            Type = _type,
            Value = _vlaue,
        };

    public ForNetworkChunnel prepareForWriteing(float value) =>
        _convertToBytes(value);
}
