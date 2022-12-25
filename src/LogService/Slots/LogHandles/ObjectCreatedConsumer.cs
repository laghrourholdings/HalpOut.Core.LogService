using CommonLibrary.AspNetCore.Contracts.Objects;
using CommonLibrary.AspNetCore.Logging.LoggingService;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using LogService.Slots.LogHandles.Utilities;
using MassTransit;

namespace LogService.Slots.LogHandles;

public class ObjectCreatedConsumer : IConsumer<ObjectCreated>
{
    
    private readonly IRepository<LogHandle> _handleRepository;
    private readonly ILoggingService _loggingService;
    private readonly IConfiguration _configuration;

    public ObjectCreatedConsumer(
        IRepository<LogHandle> handleRepository,
        ILoggingService loggingService,
        IConfiguration configuration)
    {
        _handleRepository = handleRepository;
        _loggingService = loggingService;
        _configuration = configuration;
    }

    
    public async Task Consume(ConsumeContext<ObjectCreated> context)
    {
        var logHandle = await LogHandleSlotUtility.GenerateLogHandleAsync(
            context.Message.ObjectId, "Internal Object",
            _configuration, _loggingService, _handleRepository);
        await context.RespondAsync(new UpdateObjectLogHandle(logHandle.ObjectId, logHandle.Id));
    }
}