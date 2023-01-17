using AutoMapper;
using CommonLibrary.AspNetCore.Logging;
using CommonLibrary.Core;
using CommonLibrary.Logging.Models.Dtos;
using LogService.Logging.Models;
using MassTransit;

namespace LogService.Logging;

public class CreateLogMessageConsumer : IConsumer<CreateLogMessage>
{
    private readonly IMapper _mapper;
    private readonly IRepository<LogHandle> _repository;

    public CreateLogMessageConsumer(
        IMapper mapper,
        IRepository<LogHandle> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    
    public async Task Consume(ConsumeContext<CreateLogMessage> context)
    {
        //TODO: Checks
        var logMessage = context.Message.LogMessage;
        var logHandleId = context.Message.LogHandleId;
        var logMessageEntity = _mapper.Map<LogMessageDto,LogMessage>(logMessage);
        var logHandle = await _repository.GetAsync(x => x.LogHandleId == logHandleId);
        if (logHandle != null)
        {
            logHandle.Messages.Add(logMessageEntity);
            await _repository.UpdateAsync(logHandle);
        }
    }
}