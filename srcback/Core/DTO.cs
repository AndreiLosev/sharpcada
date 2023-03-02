namespace sharpcada.Core.DTO;

public struct ForDeviceParametr
{
    public long  ParamAddres { init; get; }
    public byte[] Value { init; get; }
}

public struct ForNetworkChnel
{
    public long  ChanelAddres { init; get; }
    public float Value { init; get; }
}
