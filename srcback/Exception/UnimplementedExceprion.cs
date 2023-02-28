namespace sharpcada.Exception;

[System.Serializable]
public class UnimplementedExceprion : System.Exception
{
    public UnimplementedExceprion()
        : base("This feature has not yet been implemented") { }

    public UnimplementedExceprion(System.Exception inner)
        : base("This feature has not yet been implemented", inner) { }

    protected UnimplementedExceprion(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

