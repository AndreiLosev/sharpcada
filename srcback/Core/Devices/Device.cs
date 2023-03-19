using EntityDevice = sharpcada.Data.Entities.Device;

namespace sharpcada.Core.Devices;

public abstract class Device<T> where T : class
{
    protected string _name;
    protected string _ipAddres;
    protected Dictionary<long, Contracts.INetworkChannel<T>> _writeNetworkChannels;
    protected Dictionary<long, Contracts.INetworkChannel<T>> _readNetworkChannels;
    protected Dictionary<long, DeviceParameter> _parameters;
    protected T _client;

    public Device(
        EntityDevice device,
        Dictionary<long, Contracts.INetworkChannel<T>> networkChannels,
        Dictionary<long, DeviceParameter> deviceParameters,
        T client)
    {
        _name = device.Name;
        _ipAddres = device.IpAddres;
        _client = client;
        _parameters = deviceParameters;
        _writeNetworkChannels = networkChannels
            .Where(c => c.Value.IsWrite())
            .ToDictionary(c => c.Key, c => c.Value);
        _readNetworkChannels = networkChannels
            .Where(c => c.Value.IsRead())
            .ToDictionary(c => c.Key, c => c.Value);

    }
}
