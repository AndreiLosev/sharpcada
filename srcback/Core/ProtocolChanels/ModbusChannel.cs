using EntityModbusChanel = sharpcada.Data.Entities.ModbusChannel;
using ModbusFnCode = sharpcada.Core.Enams.ModbusFunctionCode;
using sharpcada.Core.Contracts;
using sharpcada.Core.DTO;
using sharpcada.Exception.Core.Modbus;
using sharpcada.Exception;
using srcback.Core.Helpers;

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

    public async Task<ForDeviceParametr[]> ReadAsync(IModbus client)
    {
        if (_length is null)
        {
            throw new ModbusReadLenthIsNullExceprion();
        }

        var bytes = _functionCode switch
        {
            ModbusFnCode.ReadCoils =>
                await client.ReadCoilsAsync(_dataAddres, _length.Value),
            ModbusFnCode.ReadInputs =>
                await client.ReadInputAsync(_dataAddres, _length.Value),
            ModbusFnCode.ReadHoldingRegisters =>
                await client.ReadHoldingRegistersAsync(_dataAddres, _length.Value),
            ModbusFnCode.ReadInputRegisters =>
                await client.ReadInputRegistersAsync(_dataAddres, _length.Value),
            _ => throw new UnimplementedExceprion(),
        };

        return _whereAndWhatToSend(bytes);
    }

    public async Task WriteAsync(IModbus client, ForNetworkChunnel[] forChannel)
    {
        switch (_functionCode)
        {
            case ModbusFnCode.WriteSingleCoil:
                var singleBit = forChannel.First().Value.First().GetBit(0);
                await client.WriteSingleCoilAsync(_dataAddres, singleBit);
                break;
            case ModbusFnCode.WriteMultipleCoils:
                await client.WriteMultipleCoilsAsync(_dataAddres, new bool[] {});
                break;
            case ModbusFnCode.WriteSingleRegister:
                var singleRegister = BitConverter.ToUInt16(forChannel.First().Value);
                await client.WriteSingleRegisterAsync(_dataAddres, singleRegister);
                break;
            case ModbusFnCode.WriteMultipleRegisters:
                await client.WritetMultipleRegisters(_dataAddres, new ushort[] {});
                break;
        }
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

