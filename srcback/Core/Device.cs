using EntityDevice = sharpcada.Data.Entities.Device;
using EntityDevicesChanels = sharpcada.Data.Entities.DevParameterNetChannel;

namespace sharpcada.Core;

public struct Device<T> where T : class
{
    private string _name;
    private string _ipAddres;
    private Contracts.INetworkChannel<T>[] _networkChannels;
    private DeviceParameter[] _parameters;
    private ChanelsParametrs[] _chanelsParametrs;
    private T _client;

    public Device(
        EntityDevice device,
        ICollection<EntityDevicesChanels> chanelsPrams,
        Contracts.INetworkChannel<T>[] networkChannels,
        T client)
    {
        _name = device.Name;
        _ipAddres = device.IpAddres;
        _client = client;
        _networkChannels = networkChannels;

        _parameters = device.Parameters is null
            ? new DeviceParameter[] {}
            : device.Parameters
                .Select(d => new DeviceParameter(d))
                .ToArray();

        _chanelsParametrs = chanelsPrams
            .Select(c => new ChanelsParametrs(c))
            .ToArray();
    }
}
