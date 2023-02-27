namespace sharpcada.Exception;

[System.Serializable]
public class SettingsDataAcquisitionException : System.Exception
{
    public SettingsDataAcquisitionException(string key)
        : base($"There is no \"{key}\" key in settings") {}

    public SettingsDataAcquisitionException(string key, System.Exception inner)
        : base($"There is no \"{key}\" key in settings", inner) {}

    protected SettingsDataAcquisitionException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
