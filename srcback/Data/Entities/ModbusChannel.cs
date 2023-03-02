using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using sharpcada.Core.Enams;

namespace sharpcada.Data.Entities;

[Table("ModbusChannels")]
public class ModbusChannel : NetworkChannel
{
    public uint DataAddres { set; get; }
    public ModbusFunctionCode FunctionCode { set; get; }
    public ushort? Length { set; get; }
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
            .Property(m => m.DataAddres)
            .IsRequired();

        modelBuilder.Entity<ModbusChannel>()
            .Property(m => m.FunctionCode)
            .IsRequired()
            .HasDefaultValue(ModbusFunctionCode.ReadHoldingRegisters);
    }
}
