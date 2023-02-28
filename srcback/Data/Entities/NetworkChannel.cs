using Microsoft.EntityFrameworkCore;

namespace sharpcada.Data.Entities;

public class NetworkChannel : EntityBaseWhitDate
{
    public long DeviceId { get; set; }
    public Device? Device { get; set; }
    public ICollection<DeviceParameter> DeviceParameters { get; set; } = null!;
    public ICollection<DevParameterNetChannel> DevParameterNetChannels { get; set; } = null!;
}

public static class ModelBuilderForNetworkChannelExtension
{
    public static void SetPropetyToNetworkChannelEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NetworkChannel>()
            .UseTptMappingStrategy();
    }
}
