using EntityModbusChannel = sharpcada.Data.Entities.ModbusChannel;
using EntityNetworkChannel = sharpcada.Data.Entities.NetworkChannel;
using EntityMOdbusDevice = sharpcada.Data.Entities.ModbusDevice;
using EntityDevParamNetChannel = sharpcada.Data.Entities.DevParameterNetChannel;
using sharpcada.Core.ProtocolChanels;
using sharpcada.Core.Contracts;
using sharpcada.Core.DTO;

namespace sharpcada.Core.Factories;

public class NetworkChanelFactory : ICoreFactory
{
    public Contracts.INetworkChannel<IModbus> Create(EntityModbusChannel channel)
    {
        var fn = this.createPostProcess(channel.DevParameterNetChannels);

        var mDevice = channel.Device switch
        {
            EntityMOdbusDevice => (EntityMOdbusDevice)channel.Device,
            _ => throw new System.Exception(), //TODO
        };
        return new ModbusChannel(channel, mDevice.ByteOrder, fn);
    }

    public Contracts.INetworkChannel<IProfiNet> Create(EntityNetworkChannel channel) //TODO ProfinetChannel
    {
        var fn = this.createPostProcess(channel.DevParameterNetChannels);
        return new ProfinetChannel(channel, fn);
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

    private Func<byte[], ForDeviceParametr[]> createPostProcess(
        ICollection<EntityDevParamNetChannel> entitys) =>
        (byte[] data) => entitys.Select(i => new ForDeviceParametr(i, data)).ToArray();
}
