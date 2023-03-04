using sharpcada.Data.Entities;
using sharpcada.Core.Enams;
using sharpcada.Exception;
namespace sharpcada.Core.DTO;

public struct ForDeviceParametr
{
    public long  ParamAddres { init; get; }
    public byte[] Value { init; get; }

    public ForDeviceParametr(
        PreparationConversionParameters preparation,
        byte[] data)
    {
        ParamAddres = preparation.ParameterId;
        if (preparation.Length == 0)
        {
            throw new UnimplementedExceprion(); //TODO;
        }

        Value = data
            .Skip(preparation.IndexNumber)
            .Take(preparation.Length)
            .ToArray();

    }
}

public struct ForNetworkChunnel
{
    public long  ChanelAddres { init; get; }
    public float Value { init; get; }
}

public struct PreparationConversionParameters
{
    public long ParameterId;
    public byte Length;
    public ushort IndexNumber;
    public ushort BitIndexNumber;

    public PreparationConversionParameters(
        DevParameterNetChannel paramChannel)
    {
        ParameterId = paramChannel.DeviceParameterId;
    
        if (paramChannel.DeviceParameter is null) {
            return;
        }

        Length = paramChannel.DeviceParameter.Type switch
        {
            ParameterType.Bool => 0,
            ParameterType.Int8 => 1,
            ParameterType.Int16 => 2,
            ParameterType.Int32 => 4,
            ParameterType.Int64 => 8,
            ParameterType.Uint8 => 1,
            ParameterType.Uint16 => 2,
            ParameterType.Uint32 => 4,
            ParameterType.Uint64 => 8,
            ParameterType.Float32 => 4,
            ParameterType.Float64 => 8,
            _ => throw new UnimplementedExceprion(),
        };

        IndexNumber = paramChannel.IndexNumber;
        BitIndexNumber = paramChannel.BitIndexNumber;
    }
}
