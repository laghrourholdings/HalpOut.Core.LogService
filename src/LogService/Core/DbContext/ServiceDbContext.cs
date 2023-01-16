using CommonLibrary.AspNetCore.Core;
using CommonLibrary.Core;
using LogService.Logging.Models;
using Microsoft.EntityFrameworkCore;
using LogHandle = LogService.Logging.Models.LogHandle;

namespace LogService.Core;

public class ServiceDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ServiceDbContext(DbContextOptions<ServiceDbContext> opt, IConfiguration configuration) : base(opt)
    {
            _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ServiceSettings serviceSettings = _configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>() ?? throw new InvalidOperationException("ServiceSettings is null");
        optionsBuilder.UseNpgsql(serviceSettings.PostgresConnectionString);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.BuildCommonLibrary();
    }
    public DbSet<LogHandle> LogHandles { get; set; }
    public DbSet<LogMessage> LogMessages { get; set; }
}