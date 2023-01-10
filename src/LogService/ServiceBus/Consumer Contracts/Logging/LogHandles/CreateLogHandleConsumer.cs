using CommonLibrary.AspNetCore.Logging.LoggingService;
using CommonLibrary.AspNetCore.ServiceBus.Contracts.Logging;
using CommonLibrary.Core;
using CommonLibrary.Logging.Models;
using LogService.ServiceBus.Consumer_Contracts.Logging.LogHandles.Utilities;
using MassTransit;

namespace LogService.ServiceBus.Consumer_Contracts.Logging.LogHandles;

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
        await LogHandleSlotUtility.GenerateLogHandleAsync(context.Message.logHandleId,
            context.Message.ObjectId, context.Message.ObjectType,
            _configuration, _loggingService, _handleRepository);
    }
}