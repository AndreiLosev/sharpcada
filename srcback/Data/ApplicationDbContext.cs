using Microsoft.EntityFrameworkCore;
using sharpcada.Data.Entities;


namespace sharpcada.Data;


public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {}

    public DbSet<Device> Devices => Set<Device>(); 

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    private void UpdateStructure(ModelBuilder modelBuilder)
    {
        modelBuilder.SetPropetyToDeviceEntity();
    }
}
