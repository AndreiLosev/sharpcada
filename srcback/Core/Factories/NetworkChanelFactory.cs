using EntityModbusChannel = sharpcada.Data.Entities.ModbusChannel;
using EntityNetworkChannel = sharpcada.Data.Entities.NetworkChannel;
using sharpcada.Core.ProtocolChanels;
using sharpcada.Core.Contracts;
using sharpcada.Core.DTO;

namespace sharpcada.Core.Factories;

public class NetworkChanelFactory : ICoreFactory
{
    public Contracts.INetworkChannel<IModbus> Create(EntityModbusChannel channel)
    {
        var fn = (byte[] data) =>
            channel.DevParameterNetChannels
                .Select(i => new ForDeviceParametr(i, data))
                .ToArray();

        return new ModbusChannel(channel, fn);
    }

    public Contracts.INetworkChannel<IProfiNet> Create(EntityNetworkChannel channel) //TODO ProfinetChannel
    {
        var fns = () => new List<ForDeviceParametr>();
        return new ProfinetChannel(channel, fns);
    }

    public Dictionary<long, Contracts.INetworkChannel<IModbus>> CreateDictionary(
        ICollection<EntityModbusChannel> channels)
    {
        return channels.ToDictionary(
            i => i.Id,
            i => this.Create(i));
    }

    public Dictionary<long, Contracts.INetworkChannel<IProfiNet>> CreateDictionary(
        ICollection<EntityNetworkChannel> channels)
    {
        return channels.ToDictionary(
            i => i.Id,
            i => this.Create(i));
    }
}
