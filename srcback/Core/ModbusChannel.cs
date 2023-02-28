using EntityModbusChanel = sharpcada.Data.Entities.ModbusChannel;

namespace sharpcada.Core;

public struct ModbusChannel : Contracts.INetworkChannel
{
    private ushort _deviceAddres;
    private uint _dataAddres;
    private ModbusFunctionCode _functionCode;
    private ushort _port;
    private ushort? _length;
    private ByteOrder _byteOrder;

    public long Id { get; init; }

    public ModbusChannel(EntityModbusChanel chanel)
    {
        Id = chanel.Id;
        _deviceAddres = chanel.DeviceAddres;
        _dataAddres = chanel.DataAddres;
        _functionCode = chanel.FunctionCode;
        _port = chanel.Port;
        _length = chanel.Length;
        _byteOrder = chanel.ByteOrder;
    }
}

