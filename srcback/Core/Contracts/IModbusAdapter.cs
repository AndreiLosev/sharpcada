namespace sharpcada.Core.Contracts;

public interface IModbus
{
    public Task<byte[]> ReadCoilsAsync(ushort firstAddress, ushort length);
    public Task<byte[]> ReadInputAsync(ushort firstAddress, ushort length);
    public Task<byte[]> ReadHoldingRegistersAsync(ushort firstAddress, ushort length);
    public Task<byte[]> ReadInputRegistersAsync(ushort firstAddress, ushort length);
    public Task WriteSingleCoilAsync(ushort firstAddress, bool value);
    public Task WriteSingleRegisterAsync(ushort firstAddress, ushort value);
    public Task WriteMultipleCoilsAsync(ushort firstAddress, bool[] values);
    public Task WritetMultipleRegisters(ushort firstAddress, ushort[] values);

    public void Connect(string idAddres, ushort port);
}
