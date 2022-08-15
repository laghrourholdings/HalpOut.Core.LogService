using CommonLibrary.AspNetCore;
using CommonLibrary.Core;
using LogService.EFCore;
using LogService.LogHandle;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var logger = new LoggerConfiguration().WriteTo.Console();
builder.Services.AddCommonLibrary(builder.Configuration, builder.Logging, logger , MyAllowSpecificOrigins);
builder.Services.AddScoped<IRepository<LogHandle>, LogHandleRepository>();
builder.Services.AddDbContext<ServiceDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCommonLibrary(MyAllowSpecificOrigins);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();