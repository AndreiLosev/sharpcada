using EntityNetworkChannel = sharpcada.Data.Entities.NetworkChannel;
using EntityModbusChanel = sharpcada.Data.Entities.ModbusChannel;
using sharpcada.Exception;

namespace sharpcada.Core;

public static class NetworkChannelExtensions
{
    public static Contracts.INetworkChannel CreateChanel(this EntityNetworkChannel chanel)
    {
        var result = chanel switch
        {
            EntityModbusChanel {} => new ModbusChannel((EntityModbusChanel)chanel),
            _ => throw new UnimplementedExceprion(),
        };

        return result;
    }
}
