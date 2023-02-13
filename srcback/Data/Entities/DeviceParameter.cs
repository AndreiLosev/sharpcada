
using Microsoft.EntityFrameworkCore;

namespace sharpcada.Data.Entities;

public class DeviceParameter : EntityBase
{
    public string? Name { get; set; }
    public string? Unit { get; set; }
    public ParameterType Type { get; set; }
    public float CastK { get; set; }
    public float CastB { get; set; }
    public ulong DeviceId { get; set; }
    public Device? Device { get; set; }
}

public enum ParameterType
{
    Bool = 0,
    Int8 = 1,
    Int16 = 2,
    Int32 = 4,
    Int64 = 8,
    Uint8 = 16,
    Uint16 = 32,
    Uint32 = 64,
    Uint64 = 128,
    Float32 = 256,
    Float64 = 512,
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

