using FluentModbus;
using EntityModbusChanel = sharpcada.Data.Entities.ModbusChannel;
using ModbusFnCode = sharpcada.Core.Enams.ModbusFunctionCode;
using sharpcada.Core.Enams;
using sharpcada.Exception;

namespace sharpcada.Core;

public struct ModbusChannel : Contracts.INetworkChannel<ModbusTcpClient>
{
    private ushort _deviceAddres;
    private uint _dataAddres;
    private ModbusFnCode _functionCode;
    private ushort _port;
    private ushort? _length;
    private ByteOrder _byteOrder;
    private Func<object, byte[]> _convertToDevParamValue;

    public long Id { get; init; }

    public ModbusChannel(
        EntityModbusChanel chanel,
        Func<object, byte[]> convertToDevParamValue)
    {
        Id = chanel.Id;
        _deviceAddres = chanel.DeviceAddres;
        _dataAddres = chanel.DataAddres;
        _functionCode = chanel.FunctionCode;
        _port = chanel.Port;
        _length = chanel.Length;
        _byteOrder = chanel.ByteOrder;
        _convertToDevParamValue = convertToDevParamValue;
    }

    public async Task<byte[]> ReadAsync(ModbusTcpClient client)
    {
        //TODO
        await Task.Delay(1);

        return new byte[] {};
    }

    public async Task WriteAsync(ModbusTcpClient client)
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

