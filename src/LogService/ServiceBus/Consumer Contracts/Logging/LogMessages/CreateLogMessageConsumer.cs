using CommonLibrary.AspNetCore.Logging.LoggingService;
using CommonLibrary.AspNetCore.ServiceBus.Contracts.Logging;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using MassTransit;

namespace LogService.ServiceBus.Consumer_Contracts.Logging.LogMessages;

public class CreateLogMessageConsumer : IConsumer<CreateLogMessage>
{
    private readonly IRepository<LogMessage> _messageRepository;
    private readonly ILoggingService _loggingService;

    public CreateLogMessageConsumer(
        IRepository<LogMessage> messageRepository,
        ILoggingService loggingService)
    {
        _messageRepository = messageRepository;
        _loggingService = loggingService;
    }

    
    public async Task Consume(ConsumeContext<CreateLogMessage> context)
    {
        //TODO: Checks
        var logMessage = context.Message.LogMessage;
        await _messageRepository.CreateAsync(logMessage);
    }
}