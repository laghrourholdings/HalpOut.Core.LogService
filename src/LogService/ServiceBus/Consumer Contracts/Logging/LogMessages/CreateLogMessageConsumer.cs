using AutoMapper;
using CommonLibrary.AspNetCore.ServiceBus.Contracts.Logging;
using CommonLibrary.Core;
using CommonLibrary.Logging.Models;
using LogService.Logging.Models;
using MassTransit;

namespace LogService.ServiceBus.Consumer_Contracts.Logging.LogMessages;

public class CreateLogMessageConsumer : IConsumer<CreateLogMessage>
{
    private readonly IMapper _mapper;
    private readonly IRepository<LogMessage> _messageRepository;

    public CreateLogMessageConsumer(
        IMapper mapper,
        IRepository<LogMessage> messageRepository)
    {
        _mapper = mapper;
        _messageRepository = messageRepository;
    }

    
    public async Task Consume(ConsumeContext<CreateLogMessage> context)
    {
        //TODO: Checks
        var logMessage = context.Message.LogMessage;
        await _messageRepository.CreateAsync(_mapper.Map<LogMessage>(logMessage));
    }
}