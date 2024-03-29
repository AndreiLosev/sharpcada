using EntityModbusChanel = sharpcada.Data.Entities.ModbusChannel;
using ModbusFnCode = sharpcada.Core.Enams.ModbusFunctionCode;
using sharpcada.Core.Contracts;
using sharpcada.Core.DTO;
using sharpcada.Exception.Core.Modbus;
using sharpcada.Exception;
using sharpcada.Core.Helpers;
using sharpcada.Core.Enams;

namespace sharpcada.Core.ProtocolChanels;

public class ModbusChannel : Contracts.INetworkChannel<IModbus>
{
    private uint _dataAddres;
    private ModbusFnCode _functionCode;
    private ushort? _length;
    private Func<byte[], ForDeviceParametr[]> _postProcess;
    private ByteOrder _byteOrder;

    public ModbusChannel(
        EntityModbusChanel chanel,
        ByteOrder byteOrder,
        Func<byte[], ForDeviceParametr[]> postProcess)
    {
        _dataAddres = chanel.DataAddres;
        _functionCode = chanel.FunctionCode;
        _length = chanel.Length;
        _byteOrder = byteOrder;
        _postProcess = postProcess;
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

        var result = _byteOrder switch
        {
            ByteOrder.BigEndian => _postProcess(bytes),
            ByteOrder.LittleEndian => _postProcess(bytes)
                .Select(i => i.Value.Length switch
                {
                    1 => i,
                    _ => new ForDeviceParametr(i.Value.Reverse().ToArray(), i.ParamAddres),
                }).ToArray(),
                _ => throw new UnimplementedExceprion(),
            };

        return result;
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
                var multipleBits = this.createMultipleCoilsValue(forChannel);
                await client.WriteMultipleCoilsAsync(_dataAddres, multipleBits);
                break;
            case ModbusFnCode.WriteSingleRegister:
                var singleRegister = _byteOrder switch
                {
                    ByteOrder.BigEndian => BitConverter.ToUInt16(forChannel.First().Value),
                    ByteOrder.LittleEndian =>
                        BitConverter.ToUInt16(forChannel.First().Value.Reverse().ToArray()),
                    _ => throw new UnimplementedExceprion(),
                };
                await client.WriteSingleRegisterAsync(_dataAddres, singleRegister);
                break;
            case ModbusFnCode.WriteMultipleRegisters:
                var multipleRegisters = this.createMultipleRegisterValue(forChannel);
                await client.WritetMultipleRegisters(_dataAddres, multipleRegisters);
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

    private bool[] createMultipleCoilsValue(ForNetworkChunnel[] forChannel) =>
        forChannel
            .OrderBy(i => i.IndexNumber)
            .Select(i => i.Value.First().GetBit(0))
            .ToArray();

    private ushort[] createMultipleRegisterValue(ForNetworkChunnel[] forNetworks)
    {
        var bits = forNetworks
            .Where(i => i.isBit())
            .GroupBy(i => i.IndexNumber)
            .Select(g => this.createRegisterFromBits(g.ToArray()));

        var bytes = forNetworks
            .Where(i => i.isByte())
            .GroupBy(i => i.IndexNumber)
            .Select(g => this.createRegisterFromBytes(g.ToArray()));

        var registesrs = forNetworks
            .Where(i => !i.isByte() && !i.isBit())
            .Concat(bits)
            .Concat(bytes)
            .OrderBy(i => i.IndexNumber)
            .Select(i => i.Value);

        var result = new List<ushort>();

        foreach (var regBytes in registesrs)
        {
            var values = regBytes.Chunk(2)
                .Select(i => _byteOrder switch
                {
                    ByteOrder.BigEndian => BitConverter.ToUInt16(i),
                    ByteOrder.LittleEndian =>
                        BitConverter.ToUInt16(i.Reverse().ToArray()),
                    _ => throw new UnimplementedExceprion(),
                });
            foreach (var value in values)
            {
                result.Add(value);
            }
        }

        return result.ToArray();
    }

    private ForNetworkChunnel createRegisterFromBits(ForNetworkChunnel[] forChannel)
    {
        var result = (ushort)forChannel.Aggregate(0, (acc, i) =>
        {
            acc.SetBit(i.BitIndexNumber, i.Value.First().GetBit(0));
            return acc;
        });

        return new ForNetworkChunnel(result, forChannel.First());
    }

    private ForNetworkChunnel createRegisterFromBytes(ForNetworkChunnel[] forChannel)
    {
        var result = (ushort)forChannel.Aggregate(0, (acc, i) =>
        {
            byte pos = i.BitIndexNumber switch
            {
                ForNetworkChunnel.FIRST_BYTE => 0,
                ForNetworkChunnel.SECOND_BYTE => 1,
                _ => throw new UnimplementedExceprion(),
            };
            acc.SetByte(pos, i.Value.First());
            return acc;       
        });

        return new ForNetworkChunnel(result, forChannel.First());
    }

    private async Task<byte[]> setByteOrder(Func<Task<byte[]>> fn)
    {
        var response = await fn();


        return response;
    }
}

