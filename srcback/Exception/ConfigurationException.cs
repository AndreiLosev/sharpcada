namespace sharpcada.Exception;

[System.Serializable]
public class ConfigurationException : System.Exception
{
    public ConfigurationException()
        : base("configuration missing from service container") { }

    public ConfigurationException(System.Exception inner)
        : base("Configuration missing from service container", inner) { }

    protected ConfigurationException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

public static class ConfigurationExtensions
{
    public static IConfiguration CheckConfig(this IConfiguration? appConfig)
    {
        if (appConfig is null)
        {
            throw new ConfigurationException();
        }

        return appConfig;
    }

}
