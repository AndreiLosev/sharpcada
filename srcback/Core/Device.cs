using EntityDevice = sharpcada.Data.Entities.Device;
using EntityDevicesChanels = sharpcada.Data.Entities.DevParameterNetChannel;

namespace sharpcada.Core;

public struct Device<T> where T : class
{
    private string _name;
    private string _ipAddres;
    private Dictionary<long, Contracts.INetworkChannel<T>> _networkChannels;
    private Dictionary<long, DeviceParameter> _parameters;
    private ChanelsParametrs[] _chanelsParametrs;
    private T _client;

    public Device(
        EntityDevice device,
        ICollection<EntityDevicesChanels> chanelsPrams,
        Dictionary<long, Contracts.INetworkChannel<T>> networkChannels,
        Dictionary<long, DeviceParameter> deviceParameter,
        T client)
    {
        _name = device.Name;
        _ipAddres = device.IpAddres;
        _client = client;
        _networkChannels = networkChannels;
        _parameters = deviceParameter;

        _chanelsParametrs = chanelsPrams
            .Select(c => new ChanelsParametrs(c))
            .ToArray();
    }
}
