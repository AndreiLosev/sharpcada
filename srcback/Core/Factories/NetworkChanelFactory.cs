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
        var prepared = channel.DevParameterNetChannels
            .Select(i => new PreparationConversionParameters(i));

        var fns1 = (byte[] data) =>
            prepared.Select(i => new ForDeviceParametr(i, data));

        var fns = (byte[] x) => new List<ForDeviceParametr>();
        return new ModbusChannel(channel, fns);
    }

    public ProfinetChannel Create(EntityNetworkChannel channel) //TODO ProfinetChannel
    {
        var fns = () => new List<ForDeviceParametr>();
        return new ProfinetChannel(channel, fns);
    }
}
