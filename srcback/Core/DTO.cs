using EntityNetChanelDevParam = sharpcada.Data.Entities.DevParameterNetChannel;
using sharpcada.Core.Enams;
using sharpcada.Exception;

namespace sharpcada.Core.DTO;

public struct ForDeviceParametr
{
    public long  ParamAddres { init; get; }
    public byte[] Value { init; get; }

    public ForDeviceParametr(
        EntityNetChanelDevParam paramChannel,
        byte[] data)
    {
        var length = paramChannel.DeviceParameter.Type switch
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

        ParamAddres = paramChannel.DeviceParameterId;

        if (length == 0)
        {
            Value = new byte[]
            {
                data.Skip(paramChannel.IndexNumber)
                    .Take(1)
                    .First(),
                paramChannel.BitIndexNumber,
            };
            return;
        }

        Value = data
            .Skip(paramChannel.IndexNumber)
            .Take(length)
            .ToArray();

    }
}

public struct ForNetworkChunnel
{
    public long  ChanelAddres { init; get; }
    public byte[] Value { init; get; }
    public ushort IndexNumber { init; get; }
    public byte BitIndexNumber { init; get; }

    public ForNetworkChunnel(
        float value,
        long addres,
        ParameterType type,
        ushort indexNumber,
        byte bitIndexNumber)
    {
        ChanelAddres = addres;
        Value = type switch
        {
            ParameterType.Bool => value > 0.5 ? new byte[] { 1 } : new byte[] { 0 },
            ParameterType.Int8 => new byte[] { (byte) value },
            ParameterType.Uint8 => new byte[] { (byte) value },
            ParameterType.Int16 => BitConverter.GetBytes((short)value),
            ParameterType.Uint16 => BitConverter.GetBytes((ushort)value),
            ParameterType.Int32 => BitConverter.GetBytes((int)value),
            ParameterType.Uint32 => BitConverter.GetBytes((uint)value),
            ParameterType.Int64 => BitConverter.GetBytes((long)value),
            ParameterType.Uint64 => BitConverter.GetBytes((ulong)value),
            ParameterType.Float32 => BitConverter.GetBytes(value),
            ParameterType.Float64 => BitConverter.GetBytes((double)value),
            _ => throw new UnimplementedExceprion(),
        };
        IndexNumber = indexNumber;
        BitIndexNumber = bitIndexNumber;
    }    
}
