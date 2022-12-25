using CommonLibrary.AspNetCore.Contracts.Logging;
using CommonLibrary.AspNetCore.Contracts.Users;
using CommonLibrary.AspNetCore.Identity.Model;
using CommonLibrary.AspNetCore.Logging;
using CommonLibrary.AspNetCore.Logging.LoggingService;
using CommonLibrary.AspNetCore.ServiceBus;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using LogService.Slots.LogHandles.Utilities;
using MassTransit;
using ILogger = Serilog.ILogger;

namespace LogService.Slots.LogHandles;

public class UserCreatedConsumer : IConsumer<UserCreated>
{
    
    private readonly IRepository<LogHandle> _handleRepository;
    private readonly ILoggingService _loggingService;
    private readonly IConfiguration _configuration;

    public UserCreatedConsumer(
        IRepository<LogHandle> handleRepository,
        ILoggingService loggingService,
        IConfiguration configuration)
    {
        _handleRepository = handleRepository;
        _loggingService = loggingService;
        _configuration = configuration;
    }

    
    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        var userId = context.Message.UserId;
        var logHandle = await LogHandleSlotUtility.GenerateLogHandleAsync(
                userId, "User", _configuration, _loggingService, _handleRepository);
        await context.RespondAsync(new UpdateUserLogHandle(context.Message.UserId, logHandle.Id));
    }
}