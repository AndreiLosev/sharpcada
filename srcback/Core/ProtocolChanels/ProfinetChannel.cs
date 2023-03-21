using EntityChanel = sharpcada.Data.Entities.NetworkChannel;
using sharpcada.Core.Contracts;
using sharpcada.Core.DTO;

namespace sharpcada.Core.ProtocolChanels;

public class ProfinetChannel : Contracts.INetworkChannel<IProfiNet>
{
    //todo
    public ProfinetChannel(
        EntityChanel chanel,
        Func<byte[], ForDeviceParametr[]> whereAndWhatToSend)
    {
        //TODO
    }

    public async Task<ForDeviceParametr[]> ReadAsync(IProfiNet client)
    {
        //TODO
        await Task.Delay(1);

        return new ForDeviceParametr[] {};
    }

    public async Task WriteAsync(IProfiNet client, ForNetworkChunnel[] forChannel)
    {
        // TODO
        await Task.Delay(1);
    }

    public bool IsRead() => true;
    public bool IsWrite() => !this.IsRead();
}

