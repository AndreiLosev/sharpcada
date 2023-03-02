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
        IModbus client) : base(device, networkChannels, deviceParameters, client)
    {
        _deviceAddres = device.DeviceAddres;
        _port = device.Port;
        _byteOrder = device.ByteOrder;
    }
}
