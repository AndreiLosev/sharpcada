using sharpcada.Core.Contracts;
using EntityDevice = sharpcada.Data.Entities.Device;

namespace sharpcada.Core.Devices;

public class ProfinetDevice : Device<IProfiNet>
{
    //TODO

    public ProfinetDevice(
        EntityDevice device,
        Dictionary<long, Contracts.INetworkChannel<IProfiNet>> networkChannels,
        Dictionary<long, DeviceParameter> deviceParameters,
        IProfiNet client) : base(
            device,
            networkChannels,
            deviceParameters,
            client)
    {
        //TODO
    }
}
