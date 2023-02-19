using Microsoft.EntityFrameworkCore;

namespace sharpcada.Data.Entities;

public class  NetworkChannelDeviceParameter
{
    public long Id { set; get; }
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
        modelBuilder.Entity<DeviceParameter>()
            .HasMany(d => d.NetworkChannels)
            .WithMany(n => n.DeviceParameters)
            .UsingEntity<NetworkChannelDeviceParameter>(
                j => j
                    .HasOne(nd => nd.NetworkChannel)
                    .WithMany(n => n.ChannelParameters)
                    .HasForeignKey(nd => nd.NetworkChannelId),
                j => j
                    .HasOne(nd => nd.DeviceParameter)
                    .WithMany(d => d.ParameterChannels)
                    .HasForeignKey(nd => nd.DeviceParameterId),
                j => 
                {
                    j.Property(nd => nd.BitIndexNumber).IsRequired();
                    j.Property(nd => nd.IndexNumber).IsRequired();
                    j.HasKey(nd => nd.Id);
                }
            );

    }
}

