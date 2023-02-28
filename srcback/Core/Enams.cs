namespace sharpcada.Core;

public enum NetworkProtocol: byte
{
    Modbus = 0,
    ProfiNet = 1,
}

public enum ModbusFunctionCode : byte
{
    ReadCoil = 1,
    ReadInput = 2,
    ReadHoldingRegisters = 3,
    ReadInputRegisters = 4,
    ForceSingleCoil = 5,
    ForceSingleRegister = 6,
    ForceMultipleCoils = 15,
    PresetMultipleRegisters = 16,
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

