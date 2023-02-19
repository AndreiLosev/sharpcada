
using Microsoft.EntityFrameworkCore;

namespace sharpcada.Data.Entities;

public class NetworkChannelDeviceParameter
{
   public ulong NetworkChannelId { set; get; }
   public NetworkChannel? Channel { set; get; }
   public ulong DeviceParameterId { set; get; }
   public DeviceParameter? Parameter { set; get; }
   public ushort IndexNumber { set; get; }
   public ushort BitIndexNumber { set; get; }
}

public static class ModelBuilderForNetworkChannelDeviceParameterExtension
{
    public static void SetPropetyToNetworkChannelDeviceParameterEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NetworkChannelDeviceParameter>()
            .HasKey(nd => new { nd.NetworkChannelId, nd.DeviceParameterId });

        modelBuilder
            .Entity<DeviceParameter>()
            .HasMany(d => d.NetworkChannels)
            .WithMany(n => n.DeviceParameters)
            .UsingEntity<NetworkChannelDeviceParameter>(
                j => j
                    .HasOne(nd => nd.Channel)
                    .WithMany(n => n.ChannelParameters)
                    .HasForeignKey(nd => nd.NetworkChannelId),
                j => j
                    .HasOne(nd => nd.Parameter)
                    .WithMany(d => d.ParameterChannels)
                    .HasForeignKey(nd => nd.DeviceParameterId),
                j => 
                {
                    j.HasKey(nd => new { nd.NetworkChannelId, nd.DeviceParameterId });
                    j.ToTable("NetworkChannelDeviceParameters");
                }
            );
    }
}

