using sharpcada.Services.DataAcquisition;


namespace sharpcada.Exception;

[System.Serializable]
public class DataAcquisitionServiceUndefinedStateException : System.Exception
{
    public DataAcquisitionServiceUndefinedStateException(
        DataAcquisitionServeiceState state)
        : base($"Undefined state: {state}") {}

    public DataAcquisitionServiceUndefinedStateException(
            DataAcquisitionServeiceState state,
            System.Exception inner)
        : base($"Undefined state: {state}", inner) {}

    protected DataAcquisitionServiceUndefinedStateException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
