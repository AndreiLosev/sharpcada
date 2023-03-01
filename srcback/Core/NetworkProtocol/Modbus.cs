// using FluentModbus;
// using FnCode = sharpcada.Core.Enams.ModbusFunctionCode;
// using sharpcada.Core.Contracts;

// namespace sharpcada.Core.NetworkProtocol;

// public struct Modbus : IModbusAdapter<ModbusTcpClient>
// {
//     public ushort DeviceAddres {get; init;}
//     public ModbusTcpClient Client {get; init;}
//         
//     public Modbus(ushort deviceAddres)
//     {
//         DeviceAddres = deviceAddres;
//         Client = new ModbusTcpClient();

//     }

//     public async Task<byte[]> ReadCoilsAsync(ushort firstAddress, ushort length)
//     {
//         var coils = await Client
//             .ReadCoilsAsync(DeviceAddres, firstAddress, length);

//         return coils.ToArray(); 
//     }

//     public async Task<byte[]> ReadInputAsync(ushort firstAddress, ushort length)
//     {
//         var inputs = await Client
//             .ReadDiscreteInputsAsync(DeviceAddres, firstAddress, length);

//         return inputs.ToArray();
//     }
//     public async Task<byte[]> ReadHoldingRegistersAsync(ushort firstAddress, ushort length)
//     {
//         var holdingRegisters = await Client
//             .ReadHoldingRegistersAsync((byte)DeviceAddres, firstAddress, length);

//         return holdingRegisters.ToArray();
//     }
//     public async Task<byte[]> ReadInputRegistersAsync(ushort firstAddress, ushort length)
//     {
//         var inputRegisters = await Client
//             .ReadInputRegistersAsync((byte)DeviceAddres, firstAddress, length);

//         return inputRegisters.ToArray();
//     }
//     public Task WriteSingleCoilAsync(ushort firstAddress, bool value);
//     public Task WriteSingleRegisterAsync(ushort firstAddress, ushort value);
//     public Task WriteMultipleCoilsAsync(ushort firstAddress, bool[] values);
//     public Task WritetMultipleRegisters(ushort firstAddress, ushort[] values);

//     public void Connect(string socket)
//     {
//         Client.Connect(socket);
//     }
// }
