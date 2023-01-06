using CommonLibrary.AspNetCore.Logging.LoggingService;
using CommonLibrary.AspNetCore.ServiceBus.Contracts.Users;
using CommonLibrary.Core;
using CommonLibrary.Logging.Models;
using LogService.ServiceBus.Consumer_Contracts.Logging.LogHandles.Utilities;
using MassTransit;

namespace LogService.ServiceBus.Consumer_Contracts.Logging.LogHandles;

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