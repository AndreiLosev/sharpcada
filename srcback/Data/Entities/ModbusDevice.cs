using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using sharpcada.Core.Enams;

namespace sharpcada.Data.Entities;

[Table("ModbusDevice")]
public class ModbusDevice : Device
{
    public ushort DeviceAddres { set; get; }
    public ushort Port { get; set; }
    public ByteOrder ByteOrder { set; get; }
}

public static class ModelBuilderForModbusDeviceExtension
{
    public static void SetPropetyToModbusDeviceEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ModbusDevice>()
            .Property(d => d.Port)
            .IsRequired();

        modelBuilder.Entity<ModbusDevice>()
            .Property(m => m.ByteOrder)
            .IsRequired()
            .HasDefaultValue(ByteOrder.LittleEndian);

        modelBuilder.Entity<ModbusDevice>()
            .Property(m => m.Port)
            .IsRequired()
            .HasDefaultValue(502);
    }
}
