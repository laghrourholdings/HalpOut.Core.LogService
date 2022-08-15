using CommonLibrary.AspNetCore.Contracts;
using CommonLibrary.AspNetCore.ServiceBus;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using LogService.LogHandle;
using MassTransit;
using ILogger = Serilog.ILogger;

namespace LogService.Slots.Gateway_Internal_Services;

public class CreateObjectConsumer : IConsumer<LogObjectCreate>
{
    
    private readonly IRepository<LogHandle.LogHandle> _handleRepository;
    private readonly ILogger _logger;

    public CreateObjectConsumer(IRepository<LogHandle.LogHandle> handleRepository, ILogger logger)
    {
        _handleRepository = handleRepository;
        _logger = logger;
    }
    public static string GetMessage(ref LoggingInterpolatedStringHandler handler) => handler.ToString();

    
    public async Task Consume(ConsumeContext<LogObjectCreate> context)
    {
        var logContext = context.Message.Payload;
        LogHandle.LogHandle logHandle = new LogHandle.LogHandle
        {
            Id = Guid.NewGuid(),
            ObjectId = logContext.Message.Subject,
            Messages = new SortedSet<LogMessage>(new []
            {
                new LogMessage
                {
                    Id = 0,
                    Message = GetMessage($"{@logContext.Message.Subject}"),
                    CreationDate = DateTimeOffset.Now,
                    Severity = logContext.Severity
                }
            })
        };
        var response = new ServiceBusMessageReponse<Guid>
        {
            Contract = nameof(LogObjectCreateResponse),
            InitialRequest = logContext.Message,
            Subject = logHandle.Id,
            Descriptor = GetMessage($"LogHandleID: {@logHandle.Id} assigned to : ${logContext.Message.Subject}")
        };
        _logger.Information("{ResponeDescriptor}", response.Descriptor);
        await _handleRepository.CreateAsync(logHandle);
        await context.RespondAsync(new LogObjectCreateResponse(response));
        //await _repository.CreateAsync(item);
    }
}