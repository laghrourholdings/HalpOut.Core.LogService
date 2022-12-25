using CommonLibrary.AspNetCore.Contracts.Logging;
using CommonLibrary.AspNetCore.Logging.LoggingService;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using LogService.Slots.LogHandles.Utilities;
using MassTransit;

namespace LogService.Slots.LogHandles;

public class CreateLogHandleConsumer : IConsumer<CreateLogHandle>
{
    
    private readonly IRepository<LogHandle> _handleRepository;
    private readonly ILoggingService _loggingService;
    private readonly IConfiguration _configuration;

    public CreateLogHandleConsumer(
        IRepository<LogHandle> handleRepository,
        ILoggingService loggingService,
        IConfiguration configuration)
    {
        _handleRepository = handleRepository;
        _loggingService = loggingService;
        _configuration = configuration;
    }

    
    public async Task Consume(ConsumeContext<CreateLogHandle> context)
    {
        var logHandle = await LogHandleSlotUtility.GenerateLogHandleAsync(
            context.Message.ObjectId, context.Message.ObjectType,
            _configuration, _loggingService, _handleRepository);
    }
}