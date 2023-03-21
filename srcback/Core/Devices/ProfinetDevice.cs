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
        IProfiNet client,
        ILogger logger) : base(
            device,
            networkChannels,
            deviceParameters,
            client,
            logger)
    {
        //TODO
    }

    protected override string _getLogMessage(System.Exception e)
    {
        return $"device: {_name} error: {e.ToString()}";
    }
}
