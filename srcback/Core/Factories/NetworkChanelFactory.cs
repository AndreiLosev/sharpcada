using EntityModbusChannel = sharpcada.Data.Entities.ModbusChannel;
using EntityNetworkChannel = sharpcada.Data.Entities.NetworkChannel;
using sharpcada.Core.ProtocolChanels;
using sharpcada.Core.Contracts;
using sharpcada.Core.DTO;

namespace sharpcada.Core.Factories;

public class NetworkChanelFactory : ICoreFactory
{
    public ModbusChannel Create(EntityModbusChannel channel)
    {
        var fns = () => new List<ForDeviceParametr>();
        return new ModbusChannel(channel, fns);
    }

    public ProfinetChannel Create(EntityNetworkChannel channel) //TODO ProfinetChannel
    {
        var fns = () => new List<ForDeviceParametr>();
        return new ProfinetChannel(channel, fns);
    }
}
