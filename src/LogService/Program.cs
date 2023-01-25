using CommonLibrary.AspNetCore.Core;
using CommonLibrary.AspNetCore.Logging;
using CommonLibrary.Core;
using LogService.Core;
using LogService.Logging;
using Microsoft.EntityFrameworkCore;
using Serilog;
using LoggingService = LogService.Logging.LoggingService;
using LogHandle = LogService.Logging.Models.LogHandle;


var builder = WebApplication.CreateBuilder(args);

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var logger = new LoggerConfiguration().WriteTo.Console();
builder.Services.AddDbContext<StoreDbContext>();
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddCommonLibrary(builder.Configuration, builder.Logging, logger , MyAllowSpecificOrigins, true, false);
//builder.Services.AddScoped<IRepository<LogMessage>, LogMessageRepository>();
builder.Services.AddScoped<ILoggingService, LoggingService>();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddScoped<IRepository<LogHandle>, LogHandleRepository>();

builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCommonLibrary(MyAllowSpecificOrigins);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<StoreDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}
app.Run();