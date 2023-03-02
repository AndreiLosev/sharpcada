using EntityModbusDevice = sharpcada.Data.Entities.ModbusDevice;
using EntityDevice = sharpcada.Data.Entities.Device;
using sharpcada.Core.Devices;
using sharpcada.Core.Contracts;

namespace sharpcada.Core.Factories;

public class DeviceFactory : ICoreFactory
{
    public ModbusDevice Create(
        EntityModbusDevice device,
        Dictionary<long, INetworkChannel<IModbus>> networkChannels,
        Dictionary<long, DeviceParameter> deviceParameters,
        IModbus client)
    {
        return new ModbusDevice(device, networkChannels, deviceParameters, client);
    }

    public ProfinetDevice Create(
        EntityDevice device,
        Dictionary<long, INetworkChannel<IProfiNet>> networkChannels,
        Dictionary<long, DeviceParameter> deviceParameters,
        IProfiNet client)
    {
        return new ProfinetDevice(device, networkChannels, deviceParameters, client);
    }
}
