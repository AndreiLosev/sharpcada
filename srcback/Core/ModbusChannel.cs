using EntityModbusChanel = sharpcada.Data.Entities.ModbusChannel;
using ModbusFnCode = sharpcada.Core.Enams.ModbusFunctionCode;
using sharpcada.Core.Enams;
using Modbus = sharpcada.Core.NetworkProtocol.Modbus;

namespace sharpcada.Core;

public struct ModbusChannel : Contracts.INetworkChannel<Modbus>
{
    private ushort _deviceAddres;
    private uint _dataAddres;
    private ModbusFnCode _functionCode;
    private ushort _port;
    private ushort? _length;
    private ByteOrder _byteOrder;
    private Func<object, byte[]> _convertToDevParamValue;

    public ModbusChannel(
        EntityModbusChanel chanel,
        Func<object, byte[]> convertToDevParamValue)
    {
        _deviceAddres = chanel.DeviceAddres;
        _dataAddres = chanel.DataAddres;
        _functionCode = chanel.FunctionCode;
        _port = chanel.Port;
        _length = chanel.Length;
        _byteOrder = chanel.ByteOrder;
        _convertToDevParamValue = convertToDevParamValue;
    }

    public async Task<byte[]> ReadAsync(Modbus client)
    {
        //TODO
        await Task.Delay(1);

        return new byte[] {};
    }

    public async Task WriteAsync(Modbus client)
    {
        
        await Task.Delay(1);
    }

    public bool IsRead() => _functionCode switch
    {
        ModbusFnCode.ReadCoils => true,
        ModbusFnCode.ReadInputs => true,
        ModbusFnCode.ReadHoldingRegisters => true,
        ModbusFnCode.ReadInputRegisters => true,
        _ => false,
    };

    public bool IsWrite() => !this.IsRead();
}

