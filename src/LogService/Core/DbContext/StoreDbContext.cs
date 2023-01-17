using CommonLibrary.Core;
using LogService.Logging.Models;
using Microsoft.EntityFrameworkCore;
using LogHandle = LogService.Logging.Models.LogHandle;

namespace LogService.Core;

public class StoreDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public StoreDbContext(DbContextOptions<StoreDbContext> opt, IConfiguration configuration) : base(opt)
    {
            _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        DatabaseSettings serviceSettings = _configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>() ?? throw new InvalidOperationException("ServiceSettings is null");
        optionsBuilder.UseNpgsql(serviceSettings.StorePostgresConnectionString);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.BuildCommonLibrary();
    }
    public DbSet<LogHandle> LogHandles { get; set; }
    public DbSet<LogMessage> LogMessages { get; set; }
}