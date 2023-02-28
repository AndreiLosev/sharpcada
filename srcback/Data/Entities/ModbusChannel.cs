using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using sharpcada.Core;

namespace sharpcada.Data.Entities;

[Table("ModbusChannels")]
public class ModbusChannel : NetworkChannel
{
    public ushort DeviceAddres { set; get; }
    public uint DataAddres { set; get; }
    public ModbusFunctionCode FunctionCode { set; get; }
    public ushort Port { get; set; }
    public ushort? Length { set; get; }
    public ByteOrder ByteOrder { set; get; }
}

public static class ModelBuilderForModbusChannelExtension
{
    public static void SetPropetyToModbusChannelEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ModbusChannel>()
            .Property(m => m.DataAddres)
            .IsRequired()
            .HasDefaultValue(1);

        modelBuilder.Entity<ModbusChannel>()
            .Property(d => d.Port)
            .IsRequired();


        modelBuilder.Entity<ModbusChannel>()
            .Property(m => m.DataAddres)
            .IsRequired();

        modelBuilder.Entity<ModbusChannel>()
            .Property(m => m.FunctionCode)
            .IsRequired()
            .HasDefaultValue(ModbusFunctionCode.ReadHoldingRegisters);

        modelBuilder.Entity<ModbusChannel>()
            .Property(m => m.ByteOrder)
            .IsRequired()
            .HasDefaultValue(ByteOrder.LittleEndian);
    }
}
