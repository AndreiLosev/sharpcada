using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using sharpcada.Data.Entities;

namespace sharpcada.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {}

    public DbSet<Device> Devices => Set<Device>(); 
    public DbSet<ModbusDevice> ModbusDevices => Set<ModbusDevice>();
    public DbSet<DeviceParameter> DeviceParameters => Set<DeviceParameter>();
    public DbSet<NetworkChannel> NetworkChannels => Set<NetworkChannel>();
    public DbSet<ModbusChannel> ModbusChannels => Set<ModbusChannel>();
    public DbSet<Meterage> Meterages => Set<Meterage>();
    public DbSet<Setting> Settings => Set<Setting>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    private void UpdateStructure(ModelBuilder modelBuilder)
    {
        modelBuilder.SetPropetyToDeviceEntity();
        modelBuilder.SetPropetyToDeviceParameterEntity();
        modelBuilder.SetPropetyToNetworkChannelEntity();
        modelBuilder.SetPropetyToNetworkChannelDeviceParameterEntity();
        modelBuilder.SetPropetyToModbusChannelEntity();
    }

    public override int SaveChanges()
    {
        this.AddTimestamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = new CancellationToken())
    {
        this.AddTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void AddTimestamps()
    {

        foreach (var entity in ChangeTracker.Entries())
        {
            if (entity.Entity is not EntityBaseWhitDate)
            {
                continue;
            }

            var baseEntity = (EntityBaseWhitDate)entity.Entity;
            if (entity.State == EntityState.Added)
            {
                baseEntity.CreatedAt = DateTime.Now;
            } else if (entity.State == EntityState.Modified)
            {
                baseEntity.UpdatedAt = DateTime.Now;
            }
        }
    }
}
