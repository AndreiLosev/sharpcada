using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

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

public enum ModbusFunctionCode : byte
{
    ReadCoil = 1,
    ReadInput = 2,
    ReadHoldingRegisters = 3,
    ReadInputRegisters = 4,
    ForceSingleCoil = 5,
    ForceSingleRegister = 6,
    ForceMultipleCoils = 15,
    PresetMultipleRegisters = 16,
}

public enum ByteOrder : byte
{
    LittleEndian = 1,
    BigEndian = 2,
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
