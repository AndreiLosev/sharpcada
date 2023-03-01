namespace sharpcada.Core.Enams;

public enum NetworkProtocol: byte
{
    Modbus = 0,
    ProfiNet = 1,
}

public enum ModbusFunctionCode : byte
{
    ReadCoils = 1,
    ReadInputs = 2,
    ReadHoldingRegisters = 3,
    ReadInputRegisters = 4,
    WriteSingleCoil = 5,
    WriteSingleRegister = 6,
    WriteMultipleCoils = 15,
    WriteMultipleRegisters = 16,
}

public enum ByteOrder : byte
{
    LittleEndian = 1,
    BigEndian = 2,
}

public enum ParameterType : byte
{
    Bool = 0,
    Int8 = 1,
    Int16 = 2,
    Int32 = 3,
    Int64 = 4,
    Uint8 = 5,
    Uint16 = 6,
    Uint32 = 7,
    Uint64 = 8,
    Float32 = 9,
    Float64 = 10,
}

