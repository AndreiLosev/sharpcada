using Microsoft.EntityFrameworkCore;

namespace sharpcada.Data.Entities;

public class DeviceParameterNetworkChannel
{
    // public long Id { set; get; }
    public long NetworkChannelId { set; get; }
    public NetworkChannel? NetworkChannel { set; get; }
    public long DeviceParameterId { set; get; }
    public DeviceParameter? DeviceParameter { set; get; }
    public ushort IndexNumber { set; get; }
    public ushort BitIndexNumber { set; get; }
}

public static class ModelBuilderForNetworkChannelDeviceParameterExtension
{
    public static void SetPropetyToNetworkChannelDeviceParameterEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<DeviceParameter>()
            .HasMany(d => d.NetworkChannels)
            .WithMany(n => n.DeviceParameters)
            .UsingEntity<DeviceParameterNetworkChannel>(
                j => j
                    .HasOne(nd => nd.NetworkChannel)
                    .WithMany(n => n.DeviceParameterNetworkChannels)
                    .HasForeignKey(nd => nd.NetworkChannelId),
                j => j
                    .HasOne(nd => nd.DeviceParameter)
                    .WithMany(d => d.DeviceParameterNetworkChannels)
                    .HasForeignKey(nd => nd.DeviceParameterId),
                j => 
                {
                    j.Property(nd => nd.BitIndexNumber).IsRequired();
                    j.Property(nd => nd.IndexNumber).IsRequired();
                    j.HasKey(nd => new { nd.NetworkChannelId, nd.DeviceParameterId });
                    // j.HasKey(nd => nd.Id);
                    j.ToTable("DeviceParameterNetworkChannel");
                }
            );
    }
}

