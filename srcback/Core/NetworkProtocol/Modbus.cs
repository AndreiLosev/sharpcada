using FluentModbus;
using FnCode = sharpcada.Core.Enams.ModbusFunctionCode;
using sharpcada.Core.Contracts;

namespace sharpcada.Core.NetworkProtocol;

public struct Modbus : IModbusAdapter
{
    public ushort DeviceAddres {get; init;}

    public Modbus(ushort deviceAddres)
    {
        DeviceAddres = deviceAddres;
    }

    public async Task<byte[]> Read(
        ushort startingAddress,
        ushort quantity,
        FnCode code)
    {
        await Task.Delay(1);
        return new byte[] {};
    }

    public async Task Write(
        ushort startingAddress,
        byte[] value,
        FnCode code)
    {
        await Task.Delay(1);
    }

}
