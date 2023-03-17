namespace sharpcada.Exception.Core.Modbus;

[System.Serializable]
public class ModbusReadLenthIsNullExceprion : System.Exception
{
    public ModbusReadLenthIsNullExceprion()
        : base("modbus read function missing data length") { }

    public ModbusReadLenthIsNullExceprion (System.Exception inner)
        : base("modbus read function missing data length", inner) { }

    protected ModbusReadLenthIsNullExceprion(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

