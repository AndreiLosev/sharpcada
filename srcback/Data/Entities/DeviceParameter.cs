
using Microsoft.EntityFrameworkCore;

namespace sharpcada.Data.Entities;

public class DeviceParameter : EntityBase
{
    public string Name { get; set; } = "";
    public string? Unit { get; set; }
    public ParameterType Type { get; set; }
    public float CastK { get; set; }
    public float CastB { get; set; }
    public long DeviceId { get; set; }
    public Device? Device { get; set; }
    public List<NetworkChannel> NetworkChannels { set; get; } = new();
    public List<NetworkChannelDeviceParameter> ParameterChannels { set; get; } = new();
    public List<Meterage> Meterages { set; get; } = new();
}

public enum ParameterType : byte
{
    Bool = 0,
    Int8 = 1,
    Int16 = 2,
    Int32 = 3,
    Int64 = 4,
    Uint8 = 5,
    Uint16 = 6,
    Uint32 = 7,
    Uint64 = 8,
    Float32 = 9,
    Float64 = 10,
}

public static class ModelBuilderForDeviceParameterExtension
{
    public static void SetPropetyToDeviceParameterEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeviceParameter>()
            .Property(d => d.Name)
            .HasColumnType("varchar(255)")
            .IsRequired();

        modelBuilder.Entity<DeviceParameter>()
            .Property(d => d.Unit)
            .HasColumnType("varchar(255)");

        modelBuilder.Entity<DeviceParameter>()
            .Property(d => d.Type)
            .IsRequired();

        modelBuilder.Entity<DeviceParameter>()
            .Property(d => d.CastK)
            .IsRequired()
            .HasDefaultValue(1);

        modelBuilder.Entity<DeviceParameter>()
            .Property(d => d.CastB)
            .IsRequired()
            .HasDefaultValue(0);
    }
}

