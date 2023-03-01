using EntityDevice = sharpcada.Data.Entities.Device;
using EntityDevicesChanels = sharpcada.Data.Entities.DevParameterNetChannel;

namespace sharpcada.Core;

public struct Device
{
    private string _name;
    private string _ipAddres;
    private Contracts.INetworkChannel[] _networkChannels;
    private DeviceParameter[] _parameters;
    private ChanelsParametrs[] _chanelsParametrs;

    public Device(EntityDevice device, ICollection<EntityDevicesChanels> chanelsPrams)
    {
        _name = device.Name;
        _ipAddres = device.IpAddres;

        _networkChannels = device.NetworkChannels is null
            ? new Contracts.INetworkChannel[] {}
            : device.NetworkChannels
                .Select(n => n.CreateChanel())
                .ToArray();

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
