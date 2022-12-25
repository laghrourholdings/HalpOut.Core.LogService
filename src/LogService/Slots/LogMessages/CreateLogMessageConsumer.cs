using CommonLibrary.AspNetCore.Contracts.Logging;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using MassTransit;
using ILogger = Serilog.ILogger;

namespace LogService.Slots.LogMessages;

public class CreateLogMessageConsumer : IConsumer<CreateLogMessage>
{
    private readonly IRepository<LogMessage> _messageRepository;
    private readonly ILogger _logger;

    public CreateLogMessageConsumer(
        IRepository<LogMessage> messageRepository,
        ILogger logger)
    {
        _messageRepository = messageRepository;
        _logger = logger;
    }

    
    public async Task Consume(ConsumeContext<CreateLogMessage> context)
    {
        //TODO: Checks
        var logMessage = context.Message.LogMessage;
        await _messageRepository.CreateAsync(logMessage);
        //await context.RespondAsync(new UpdateObjectLogHandle(response));
    }
}