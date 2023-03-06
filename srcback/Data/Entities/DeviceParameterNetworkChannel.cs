using Microsoft.EntityFrameworkCore;

namespace sharpcada.Data.Entities;

[PrimaryKey(nameof(DeviceParameterId), nameof(NetworkChannelId))]
public class DevParameterNetChannel
{
    public long DeviceParameterId { set; get; }
    public DeviceParameter DeviceParameter { set; get; } = null!;
    public long NetworkChannelId { set; get; }
    public NetworkChannel NetworkChannel { set; get; } = null!;
    public ushort IndexNumber { set; get; }
    public byte BitIndexNumber { set; get; }
}

public static class ModelBuilderForNetworkChannelDeviceParameterExtension
{
    public static void SetPropetyToNetworkChannelDeviceParameterEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<DeviceParameter>()
            .HasMany(d => d.NetworkChannels)
            .WithMany(n => n.DeviceParameters)
            .UsingEntity<DevParameterNetChannel>(
                j => j
                    .HasOne(nd => nd.NetworkChannel)
                    .WithMany(n => n.DevParameterNetChannels)
                    .HasForeignKey(nd => nd.NetworkChannelId),
                j => j
                    .HasOne(nd => nd.DeviceParameter)
                    .WithMany(d => d.DevParameterNetChannels)
                    .HasForeignKey(nd => nd.DeviceParameterId),
                j => 
                {
                    j.Property(dn => dn.IndexNumber).IsRequired().HasDefaultValue(0);
                    j.Property(dn => dn.BitIndexNumber).IsRequired().HasDefaultValue(0);
                }
            );
    }
}

