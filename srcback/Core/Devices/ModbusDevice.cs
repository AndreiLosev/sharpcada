using sharpcada.Core.Contracts;
using EntityModbusDevice = sharpcada.Data.Entities.ModbusDevice;
using sharpcada.Core.Enams;

namespace sharpcada.Core.Devices;

public class ModbusDevice : Device<IModbus>
{
    private ushort _deviceAddres;
    private ushort _port;
    private ByteOrder _byteOrder;

    public ModbusDevice(
        EntityModbusDevice device,
        Dictionary<long, Contracts.INetworkChannel<IModbus>> networkChannels,
        Dictionary<long, DeviceParameter> deviceParameters,
        IModbus client,
        ILogger logger) : base(
            device,
            networkChannels,
            deviceParameters,
            client,
            logger)
    {
        _deviceAddres = device.DeviceAddres;
        _port = device.Port;
        _byteOrder = device.ByteOrder;
    }

    protected override string _getLogMessage(System.Exception e)
    {
        return $"device: {_name} error: {e.ToString()}";
    }
}
