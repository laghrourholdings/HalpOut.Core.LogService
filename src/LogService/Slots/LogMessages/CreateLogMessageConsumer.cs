using CommonLibrary.AspNetCore.Contracts.Logging;
using CommonLibrary.AspNetCore.Logging.LoggingService;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using MassTransit;

namespace LogService.Slots.LogMessages;

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