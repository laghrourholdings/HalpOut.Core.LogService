using CommonLibrary.AspNetCore.Contracts.LogMessage;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using MassTransit;
using ILogger = Serilog.ILogger;

namespace LogService.Slots.LogMessages;

public class LogAddMessageConsumer : IConsumer<CreateLogMessage>
{
    private readonly IRepository<LogMessage> _messageRepository;
    private readonly ILogger _logger;

    public LogAddMessageConsumer(
        IRepository<LogMessage> messageRepository,
        ILogger logger)
    {
        _messageRepository = messageRepository;
        _logger = logger;
    }

    
    public async Task Consume(ConsumeContext<CreateLogMessage> context)
    {
        var payload = context.Message.Payload;
        if (payload.Subject == null)
        {
            _logger.Error("No payload or no subjects attached to payload");
            return;
        }
        await _messageRepository.CreateAsync(payload.Subject);
        //await context.RespondAsync(new UpdateObjectLogHandle(response));
    }
}