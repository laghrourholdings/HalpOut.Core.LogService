using CommonLibrary.AspNetCore;
using CommonLibrary.AspNetCore.Logging.LoggingService;
using CommonLibrary.Core;
using CommonLibrary.Logging.Models;
using LogService.EFCore;
using LogService.Logging;
using Serilog;
using LoggingService = LogService.Logging.LoggingService;
using LogHandle = LogService.Logging.Models.LogHandle;

var builder = WebApplication.CreateBuilder(args);

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var logger = new LoggerConfiguration().WriteTo.Console();
builder.Services.AddDbContext<ServiceDbContext>();
builder.Services.AddCommonLibrary(builder.Configuration, builder.Logging, logger , MyAllowSpecificOrigins);
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

app.Run();