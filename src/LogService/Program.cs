using CommonLibrary.AspNetCore;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using LogService.EFCore;
using LogService.LogHandle;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var logger = new LoggerConfiguration().WriteTo.Console();
builder.Services.AddCommonLibrary(builder.Configuration, builder.Logging, logger , MyAllowSpecificOrigins);
builder.Services.AddScoped<IRepository<LogHandle>, LogHandleRepository>();
builder.Services.AddScoped<IRepository<LogMessage>, LogMessageRepository>();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<ServiceDbContext>();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCommonLibrary(MyAllowSpecificOrigins);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();