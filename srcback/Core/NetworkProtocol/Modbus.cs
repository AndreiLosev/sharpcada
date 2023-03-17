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

    public async Task<byte[]> ReadCoilsAsync(uint firstAddress, ushort length)
    {
        var coils = await _client
            .ReadCoilsAsync(_deviceAddres, (int)firstAddress, length);

        return coils.ToArray(); 
    }

    public async Task<byte[]> ReadInputAsync(uint firstAddress, ushort length)
    {
        var inputs = await _client
            .ReadDiscreteInputsAsync(_deviceAddres, (int)firstAddress, length);

        return inputs.ToArray();
    }

    public async Task<byte[]> ReadHoldingRegistersAsync(uint firstAddress, ushort length)
    {
        var holdingRegisters = await _client
            .ReadHoldingRegistersAsync((byte)_deviceAddres, (ushort)firstAddress, length);

        return holdingRegisters.ToArray();
    }

    public async Task<byte[]> ReadInputRegistersAsync(uint firstAddress, ushort length)
    {
        var inputRegisters = await _client
            .ReadInputRegistersAsync((byte)_deviceAddres, (ushort)firstAddress, length);

        return inputRegisters.ToArray();
    }

    public async Task WriteSingleCoilAsync(uint firstAddress, bool value)
    {
        await _client.WriteSingleCoilAsync(_deviceAddres, (int)firstAddress, value);
    }

    public async Task WriteSingleRegisterAsync(uint firstAddress, ushort value)
    {
        await _client.WriteSingleRegisterAsync(_deviceAddres, (int)firstAddress, value);
    }

    public async Task WriteMultipleCoilsAsync(uint firstAddress, bool[] values)
    {
        await Task.Delay(1);
        throw new UnimplementedExceprion();
    }

    public async Task WritetMultipleRegisters(uint firstAddress, ushort[] values)
    {
        await _client.WriteMultipleRegistersAsync(_deviceAddres, (int)firstAddress, values);
    }

    public void Connect(string idAddres, ushort prot)
    {
        _client.Connect($"{idAddres}:{prot}");
    }
}
