using Microsoft.EntityFrameworkCore;

namespace LogService.EFCore;

public class ServiceDbContext : DbContext
{
    public ServiceDbContext(DbContextOptions<ServiceDbContext> opt) : base(opt)
    {
            
    }

    public DbSet<LogHandle.LogHandle> LogHandles { get; set; }
}