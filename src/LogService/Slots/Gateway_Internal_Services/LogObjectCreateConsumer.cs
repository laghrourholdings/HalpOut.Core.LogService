using CommonLibrary.AspNetCore.Contracts;
using CommonLibrary.AspNetCore.ServiceBus;
using CommonLibrary.AspNetCore.Settings;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using MassTransit;
using ILogger = Serilog.ILogger;

namespace LogService.Slots.Gateway_Internal_Services;

public class LogObjectCreateConsumer : IConsumer<LogCreateObject>
{
    
    private readonly IRepository<CommonLibrary.Logging.LogHandle> _handleRepository;
    private readonly ILogger _logger;

    public LogObjectCreateConsumer(IRepository<CommonLibrary.Logging.LogHandle> handleRepository, ILogger logger)
    {
        _handleRepository = handleRepository;
        _logger = logger;
    }

    
    public async Task Consume(ConsumeContext<LogCreateObject> context)
    {
        var logContext = context.Message.Payload;
        var messages = new List<LogMessage>();
        messages.Add(new LogMessage
        {
            Id = 0,
            Message = ServiceSettings.GetMessage($"LogCreateObject: {logContext.Message.Subject}"),
            CreationDate = DateTimeOffset.Now,
            Severity = logContext.Severity
        });
        CommonLibrary.Logging.LogHandle logHandle = new CommonLibrary.Logging.LogHandle
        {
            Id = Guid.NewGuid(),
            ObjectId = logContext.Message.Subject.Id,
            Messages = messages
        };
        var obj = logContext.Message.Subject;
        obj.LogHandleId = logHandle.Id;
        obj.LogHandle = logHandle;
        var response = new ServiceBusMessageReponse<IIObject>
        {
            Contract = nameof(LogCreateObjectResponse),
            InitialRequest = logContext.Message,
            Subject = obj,
            Descriptor = ServiceSettings.GetMessage($"LogHandleID: {{logHandle.Id}} assigned to : ${{logContext.Message.Subject}}")
        };
        _logger.Information("{ResponeDescriptor}", response.Descriptor);
        await _handleRepository.CreateAsync(logHandle);
        await context.RespondAsync(new LogCreateObjectResponse(response));
    }
}