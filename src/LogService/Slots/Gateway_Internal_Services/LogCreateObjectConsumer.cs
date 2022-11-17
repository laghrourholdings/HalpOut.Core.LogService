using CommonLibrary.AspNetCore.Contracts;
using CommonLibrary.AspNetCore.ServiceBus;
using CommonLibrary.AspNetCore.Settings;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using MassTransit;
using ILogger = Serilog.ILogger;

namespace LogService.Slots.Gateway_Internal_Services;

public class LogCreateObjectConsumer : IConsumer<ObjectCreated>
{
    
    private readonly IRepository<CommonLibrary.Logging.LogHandle> _handleRepository;
    private readonly ILogger _logger;

    public LogCreateObjectConsumer(IRepository<CommonLibrary.Logging.LogHandle> handleRepository, ILogger logger)
    {
        _handleRepository = handleRepository;
        _logger = logger;
    }

    
    public async Task Consume(ConsumeContext<ObjectCreated> context)
    {
        var payload = context.Message.Payload;
        if (payload.Subject == null)
        {
            _logger.Error("No payload or no subjects attached to payload");
            return;
        }
        CommonLibrary.Logging.LogHandle logHandle = new CommonLibrary.Logging.LogHandle
        {
            Id = Guid.NewGuid(),
            ObjectId = payload.Subject.Id,
            CreationDate = DateTimeOffset.Now,
            IsDeleted = false,
            DeletedDate = default,
            IsSuspended = false,
            SuspendedDate = default,
            Descriptor = ServiceSettings.GetMessage($"InDB Messages"),
            ObjectType = nameof(payload.Subject),
            Messages = new List<LogMessage>(),
            AuthorizationDetails = "none",
            LocationDetails = "none"
        };
        var msg = new LogMessage
        {
            Id = default,
            CreationDate = DateTimeOffset.Now,
            IsDeleted = false,
            DeletedDate = default,
            IsSuspended = false,
            SuspendedDate = default,
            LogHandleId = default,
            Descriptor = ServiceSettings.GetMessage($"Received request for object creation: {payload.Subject.Id}"),
            Severity = LogLevel.Information
        };
        logHandle.Messages.Add(msg);
        
        var obj = payload.Subject;
        obj.LogHandleId = logHandle.Id;
        var response = new ServiceBusMessageReponse<IIObject>
        {
            Contract = nameof(UpdateObjectLogHandle),
            InitialRequest = payload,
            Subject = obj,
            Descriptor = ServiceSettings.GetMessage($"LogHandleID: {logHandle.Id} assigned to : {payload.Subject}")
        };
        _logger.Information("{ResponeDescriptor}", response.Descriptor);
        await _handleRepository.CreateAsync(logHandle);
        await context.RespondAsync(new UpdateObjectLogHandle(response));
    }
}