using EntityDevice = sharpcada.Data.Entities.Device;

namespace sharpcada.Core;

public struct Device
{
    private string _name;
    private string _ipAddres;
    private Contracts.INetworkChannel[] _networkChannels;
    private DeviceParameter[] _parameters;
    private ChanelsParametrs[] _chanelsParametrs;

    public Device(EntityDevice device)
    {
        _name = device.Name;
        _ipAddres = device.IpAddres;
        _networkChannels = device.NetworkChannels is null
            ? new Contracts.INetworkChannel[] {}
            : device.NetworkChannels
                .Select(n => n.CreateChanel())
                .ToArray();
    }
}
