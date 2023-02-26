using Microsoft.EntityFrameworkCore;

namespace sharpcada.Data.Entities;

public class NetworkChannel : EntityBase
{
    public long DeviceId { get; set; }
    public Device? Device { get; set; }
    public List<DeviceParameter> DeviceParameters { get; set; } = new();
    public List<DeviceParameterNetworkChannel> DeviceParameterNetworkChannels { get; set; } = new();
}

public static class ModelBuilderForNetworkChannelExtension
{
    public static void SetPropetyToNetworkChannelEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NetworkChannel>()
            .UseTptMappingStrategy();
    }
}
