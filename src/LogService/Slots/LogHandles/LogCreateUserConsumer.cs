using CommonLibrary.AspNetCore.Contracts.Users;
using CommonLibrary.AspNetCore.Identity.Model;
using CommonLibrary.AspNetCore.Logging;
using CommonLibrary.AspNetCore.ServiceBus;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using MassTransit;
using ILogger = Serilog.ILogger;

namespace LogService.Slots.LogHandles;

public class LogCreateUserConsumer : IConsumer<UserCreated>
{
    
    private readonly IRepository<LogHandle> _handleRepository;
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;

    public LogCreateUserConsumer(
        IRepository<LogHandle> handleRepository,
        ILogger logger,
        IConfiguration configuration)
    {
        _handleRepository = handleRepository;
        _logger = logger;
        _configuration = configuration;
    }

    
    public async Task Consume(ConsumeContext<UserCreated> context)
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
            ObjectId = new Guid(payload.Subject.Id),
            CreationDate = DateTimeOffset.Now,
            IsDeleted = false,
            DeletedDate = default,
            IsSuspended = false,
            SuspendedDate = default,
            Descriptor = $"InDB Messages",
            ObjectType = nameof(User),
            AuthorizationDetails = "none",
            LocationDetails = "none"
        };
        var obj = payload.Subject;
        obj.LogHandleId = logHandle.Id;
        var response = new ServiceBusPayload<User>
        {
            Contract = nameof(UpdateUserLogHandle),
            Subject = obj,
            Descriptor = $"LogHandleID: {logHandle.Id} assigned to : {payload.Subject.Id} ({nameof(User)})"
        };
        logHandle.LogMessage(_configuration, LogLevel.None,response.Descriptor);
        _logger.Information("{ResponeDescriptor}", response.Descriptor);
        await _handleRepository.CreateAsync(logHandle);
        await context.RespondAsync(new UpdateUserLogHandle(response));
    }
}