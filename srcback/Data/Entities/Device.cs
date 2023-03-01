using Microsoft.EntityFrameworkCore;
using sharpcada.Core.Enams;

namespace sharpcada.Data.Entities;

public class Device : EntityBaseWhitDate
{
    public string Name { get; set; } = null!;
    public string IpAddres { get; set; } = null!;
    public NetworkProtocol Protocol { get; set; }
    public ICollection<DeviceParameter>? Parameters { get; set; }
    public ICollection<NetworkChannel>? NetworkChannels { get; set; }
}

public static class ModelBuilderForDeviceExtension
{
    public static void SetPropetyToDeviceEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Device>()
            .Property(d => d.Name)
            .HasColumnType("varchar(255)")
            .IsRequired();

        modelBuilder.Entity<Device>()
            .HasAlternateKey(d => d.Name);

        modelBuilder.Entity<Device>()
            .Property(d => d.IpAddres)
            .HasColumnType("varchar(63)")
            .IsRequired();

        modelBuilder.Entity<Device>()
            .Property(d => d.Protocol)
            .IsRequired();
    }
}

