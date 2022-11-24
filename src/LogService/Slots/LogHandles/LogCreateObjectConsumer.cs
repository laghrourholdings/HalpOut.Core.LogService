using CommonLibrary.AspNetCore.Contracts.Objects;
using CommonLibrary.AspNetCore.Logging;
using CommonLibrary.AspNetCore.ServiceBus;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using MassTransit;
using ILogger = Serilog.ILogger;

namespace LogService.Slots.LogHandles;

public class LogCreateObjectConsumer : IConsumer<ObjectCreated>
{
    
    private readonly IRepository<LogHandle> _handleRepository;
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;

    public LogCreateObjectConsumer(
        IRepository<LogHandle> handleRepository,
        ILogger logger,
        IConfiguration configuration)
    {
        _handleRepository = handleRepository;
        _logger = logger;
        _configuration = configuration;
    }

    
    public async Task Consume(ConsumeContext<ObjectCreated> context)
    {
        var payload = context.Message.Payload;
        if (payload.Subject == null)
        {
            _logger.Error("No payload or no subjects attached to payload");
            return;
        }
        LogHandle logHandle = new LogHandle
        {
            Id = Guid.NewGuid(),
            ObjectId = payload.Subject.Id,
            CreationDate = DateTimeOffset.Now,
            IsDeleted = false,
            DeletedDate = default,
            IsSuspended = false,
            SuspendedDate = default,
            Descriptor = $"InDB Messages",
            ObjectType = nameof(IIObject),
            AuthorizationDetails = "none",
            LocationDetails = "none"
        };
        logHandle.LogMessage(_configuration, LogLevel.None,
            $"Received request for object creation: {payload.Subject.Id}");
        var obj = payload.Subject;
        obj.LogHandleId = logHandle.Id;
        var response = new ServiceBusMessageReponse<IIObject>
        {
            Contract = nameof(UpdateObjectLogHandle),
            InitialRequest = payload,
            Subject = obj,
            Descriptor = $"LogHandleID: {logHandle.Id} assigned to : {payload.Subject}"
        };
        _logger.Information("{ResponeDescriptor}", response.Descriptor);
        await _handleRepository.CreateAsync(logHandle);
        await context.RespondAsync(new UpdateObjectLogHandle(response));
    }
}