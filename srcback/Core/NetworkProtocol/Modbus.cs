using FluentModbus;
using sharpcada.Core.Contracts;
using sharpcada.Exception;

namespace sharpcada.Core.NetworkProtocol;

public class Modbus : IModbus
{
    private ModbusTcpClient _client;
    private ushort _deviceAddres;

    public Modbus(ushort deviceAddres)
    {
        _deviceAddres = deviceAddres;
        _client= new ModbusTcpClient();

    }

    public async Task<byte[]> ReadCoilsAsync(ushort firstAddress, ushort length)
    {
        var coils = await _client
            .ReadCoilsAsync(_deviceAddres, firstAddress, length);

        return coils.ToArray(); 
    }

    public async Task<byte[]> ReadInputAsync(ushort firstAddress, ushort length)
    {
        var inputs = await _client
            .ReadDiscreteInputsAsync(_deviceAddres, firstAddress, length);

        return inputs.ToArray();
    }

    public async Task<byte[]> ReadHoldingRegistersAsync(ushort firstAddress, ushort length)
    {
        var holdingRegisters = await _client
            .ReadHoldingRegistersAsync((byte)_deviceAddres, firstAddress, length);

        return holdingRegisters.ToArray();
    }

    public async Task<byte[]> ReadInputRegistersAsync(ushort firstAddress, ushort length)
    {
        var inputRegisters = await _client
            .ReadInputRegistersAsync((byte)_deviceAddres, firstAddress, length);

        return inputRegisters.ToArray();
    }

    public async Task WriteSingleCoilAsync(ushort firstAddress, bool value)
    {
        await _client.WriteSingleCoilAsync(_deviceAddres, firstAddress, value);
    }

    public async Task WriteSingleRegisterAsync(ushort firstAddress, ushort value)
    {
        await _client.WriteSingleRegisterAsync(_deviceAddres, firstAddress, value);
    }

    public async Task WriteMultipleCoilsAsync(ushort firstAddress, bool[] values)
    {
        await Task.Delay(1);
        throw new UnimplementedExceprion();
    }

    public async Task WritetMultipleRegisters(ushort firstAddress, ushort[] values)
    {
        await _client.WriteMultipleRegistersAsync(_deviceAddres, firstAddress, values);
    }

    public void Connect(string idAddres, ushort prot)
    {
        _client.Connect($"{idAddres}:{prot}");
    }
}
