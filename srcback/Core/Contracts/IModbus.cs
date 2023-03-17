namespace sharpcada.Core.Contracts;

public interface IModbus
{
    public Task<byte[]> ReadCoilsAsync(uint firstAddress, ushort length);
    public Task<byte[]> ReadInputAsync(uint firstAddress, ushort length);
    public Task<byte[]> ReadHoldingRegistersAsync(uint firstAddress, ushort length);
    public Task<byte[]> ReadInputRegistersAsync(uint firstAddress, ushort length);
    public Task WriteSingleCoilAsync(uint firstAddress, bool value);
    public Task WriteSingleRegisterAsync(uint firstAddress, ushort value);
    public Task WriteMultipleCoilsAsync(uint firstAddress, bool[] values);
    public Task WritetMultipleRegisters(uint firstAddress, ushort[] values);

    public void Connect(string idAddres, ushort port);
}
