using EntityModbusDevice = sharpcada.Data.Entities.ModbusDevice;
using EntityModbusChannel = sharpcada.Data.Entities.ModbusChannel;
using EntityNetChannels = sharpcada.Data.Entities.NetworkChannel;
using EntityDeviceParameter = sharpcada.Data.Entities.DeviceParameter;
using EntityDevice = sharpcada.Data.Entities.Device;
using sharpcada.Core.Devices;
using sharpcada.Core.Contracts;

namespace sharpcada.Core.Factories;

public class DeviceFactory : ICoreFactory
{
    private readonly DeviceParameterFactory _deviceParameterFactory;
    private readonly NetworkChanelFactory _networkChanelFactory;
    private readonly ModbusFactory _modbusFactory;
    private readonly ProfinetFactory _profinetFactory;
    private readonly ILogger _loger;

    public DeviceFactory(
        DeviceParameterFactory deviceParameterFactory,
        NetworkChanelFactory networkChanelFactory,
        ModbusFactory modbusFactory,
        ProfinetFactory profinetFactory,
        ILogger logger)
    {
        _deviceParameterFactory = deviceParameterFactory;
        _networkChanelFactory = networkChanelFactory;
        _modbusFactory = modbusFactory;
        _profinetFactory = profinetFactory;
        _loger = logger;
    }

    public Device<IModbus> Create(
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

        return new ModbusDevice(device, modbusChannels, deviceParameters, client, _loger);
    }

    public Device<IProfiNet> Create(
        EntityDevice device,
        IProfiNet client)
    {
        var networkChannels = _networkChanelFactory.CreateDictionary(
            device.NetworkChannels ?? new EntityNetChannels[] {});

        var deviceParameters = _deviceParameterFactory.CreateDictionary(
            device.Parameters ?? new EntityDeviceParameter[] {});

        return new ProfinetDevice(device, networkChannels, deviceParameters, client, _loger);
    }

    public Dictionary<long, Device<IModbus>> CreateDictionary(
        ICollection<EntityModbusDevice> entities) =>
            entities.ToDictionary(
                e => e.Id,
                e => this.Create(e, _modbusFactory.Create()));

    public Dictionary<long, Device<IProfiNet>> CreateDictionary(
        ICollection<EntityDevice> entities) =>
            entities.ToDictionary(
                e => e.Id,
                e => this.Create(e, _profinetFactory.Create()));
}
