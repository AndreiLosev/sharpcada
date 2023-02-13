using Microsoft.EntityFrameworkCore;

namespace sharpcada.Data.Entities;

public class NetworkChannel : EntityBase
{
    public ulong DeviceId { get; set; }
    public Device? Device { get; set; }
}

public static class ModelBuilderForNetworkChannelExtension
{
    public static void SetPropetyToNetworkChannelEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NetworkChannel>()
            .UseTptMappingStrategy();
    }
}
