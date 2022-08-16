using CommonLibrary.ModelBuilders;
using Microsoft.EntityFrameworkCore;

namespace LogService.EFCore;

public class ServiceDbContext : DbContext
{
    public ServiceDbContext(DbContextOptions<ServiceDbContext> opt) : base(opt)
    {
            
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.BuildCommonLibrary();
    }
    public DbSet<CommonLibrary.Logging.LogHandle> LogHandles { get; set; }
}