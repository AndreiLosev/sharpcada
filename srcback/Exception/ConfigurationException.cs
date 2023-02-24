namespace sharpcada.Exception;

[System.Serializable]
public class ConfigurationException : System.Exception
{
    public ConfigurationException(
        string message = "Configuration missing from service container") : base(message) { }

    public ConfigurationException(
        System.Exception inner,
        string message = "Configuration missing from service container")
        : base(message, inner) { }

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
