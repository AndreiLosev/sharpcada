using EntityModbusChanel = sharpcada.Data.Entities.ModbusChannel;
using ModbusFnCode = sharpcada.Core.Enams.ModbusFunctionCode;
using sharpcada.Core.Contracts;
using sharpcada.Core.DTO;

namespace sharpcada.Core.ProtocolChanels;

public class ModbusChannel : Contracts.INetworkChannel<IModbus>
{
    private uint _dataAddres;
    private ModbusFnCode _functionCode;
    private ushort? _length;
    private Func<byte[], ForDeviceParametr[]> _whereAndWhatToSend;

    public ModbusChannel(
        EntityModbusChanel chanel,
        Func<byte[], ForDeviceParametr[]> whereAndWhatToSend)
    {
        _dataAddres = chanel.DataAddres;
        _functionCode = chanel.FunctionCode;
        _length = chanel.Length;
        _whereAndWhatToSend = whereAndWhatToSend;
    }

    public async Task<byte[]> ReadAsync(IModbus client)
    {
        //TODO
        await Task.Delay(1);

        return new byte[] {};
    }

    public async Task WriteAsync(IModbus client)
    {
        // TODO
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

