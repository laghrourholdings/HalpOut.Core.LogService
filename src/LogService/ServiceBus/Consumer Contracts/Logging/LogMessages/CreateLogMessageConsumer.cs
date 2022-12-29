using CommonLibrary.AspNetCore.Logging.LoggingService;
using CommonLibrary.AspNetCore.ServiceBus.Contracts.Logging;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using CommonLibrary.Logging.Models;
using MassTransit;

namespace LogService.ServiceBus.Consumer_Contracts.Logging.LogMessages;

public class CreateLogMessageConsumer : IConsumer<CreateLogMessage>
{
    private readonly IRepository<LogMessage> _messageRepository;

    public CreateLogMessageConsumer(
        IRepository<LogMessage> messageRepository)
    {
        _messageRepository = messageRepository;
    }

    
    public async Task Consume(ConsumeContext<CreateLogMessage> context)
    {
        //TODO: Checks
        var logMessage = context.Message.LogMessage;
        await _messageRepository.CreateAsync(logMessage);
    }
}