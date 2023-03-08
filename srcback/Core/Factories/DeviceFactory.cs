using EntityModbusDevice = sharpcada.Data.Entities.ModbusDevice;
using EntityModbusChannel = sharpcada.Data.Entities.ModbusChannel;
using EntityDeviceParameter = sharpcada.Data.Entities.DeviceParameter;
using EntityDevice = sharpcada.Data.Entities.Device;
using sharpcada.Core.Devices;
using sharpcada.Core.Contracts;

namespace sharpcada.Core.Factories;

public class DeviceFactory : ICoreFactory
{
    private readonly DeviceParameterFactory _deviceParameterFactory;
    private readonly NetworkChanelFactory _networkChanelFactory;

    public DeviceFactory(
        DeviceParameterFactory deviceParameterFactory,
        NetworkChanelFactory networkChanelFactory)
    {
        _deviceParameterFactory = deviceParameterFactory;
        _networkChanelFactory = networkChanelFactory;
    }

    public ModbusDevice Create(
        EntityModbusDevice device,
        IModbus client)
    {
        EntityModbusChannel[] enityModbusChannels = device.NetworkChannels is null
            ? new EntityModbusChannel[] {}
            : device.NetworkChannels
                .Where(n => n is EntityModbusChannel)
                .Select(n => (EntityModbusChannel)n)
                .ToArray();

        var modbusChannels = _networkChanelFactory.CreateDictionary(
            (ICollection<EntityModbusChannel>)enityModbusChannels);

        var deviceParameters = _deviceParameterFactory.CreateDictionary(
            device.Parameters ?? new EntityDeviceParameter[] {});

        return new ModbusDevice(device, modbusChannels, deviceParameters, client);
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
