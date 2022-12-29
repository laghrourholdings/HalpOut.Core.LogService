using CommonLibrary.AspNetCore.Settings;
using CommonLibrary.Logging;
using CommonLibrary.Logging.Models;
using CommonLibrary.ModelBuilders;
using Microsoft.EntityFrameworkCore;

namespace LogService.EFCore;

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