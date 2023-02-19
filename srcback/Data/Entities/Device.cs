using Microsoft.EntityFrameworkCore;

namespace sharpcada.Data.Entities;

public class Device : EntityBase
{
    public string? Name { get; set; }
    public string? IpAddres { get; set; }
    public NetworkProtocol Protocol { get; set; }
    public List<DeviceParameter> Parameters { get; set; } = new();
    public List<NetworkChannel> NetworkChannels { get; set; } = new();
}

public enum NetworkProtocol: byte
{
    Modbus = 0,
    ProfiNet = 1,
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

