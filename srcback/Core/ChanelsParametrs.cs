using EntityChanelsParametrs = sharpcada.Data.Entities.DevParameterNetChannel;

namespace sharpcada.Core;

public struct ChanelsParametrs
{
    private long _parameterId;
    private long _channelId;
    private ushort _indexNumber;
    private ushort _bitIndexNumber;

    public ChanelsParametrs(EntityChanelsParametrs entity)
    {
        _parameterId = entity.DeviceParameterId;
        _channelId = entity.NetworkChannelId;
        _indexNumber = entity.IndexNumber;
        _bitIndexNumber = entity.BitIndexNumber;
    }
}
