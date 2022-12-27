using CommonLibrary.AspNetCore.Logging.LoggingService;
using CommonLibrary.Core;
using CommonLibrary.Logging;

namespace LogService.ServiceBus.Consumer_Contracts.Logging.LogHandles.Utilities;

public static class LogHandleSlotUtility
{
    public static async Task<LogHandle> GenerateLogHandleAsync(
        Guid ObjectId,
        string ObjectType,
        IConfiguration configuration,
        ILoggingService loggingService, 
        IRepository<LogHandle> handleRepository )
    {
        LogHandle logHandle = new LogHandle
        {
            Id = Guid.NewGuid(),
            ObjectId = ObjectId,
            CreationDate = DateTimeOffset.Now,
            IsDeleted = false,
            DeletedDate = default,
            IsSuspended = false,
            SuspendedDate = default,
            Descriptor = $"InDB Messages",
            ObjectType = ObjectType,
            AuthorizationDetails = "none",
            LocationDetails = "none"
        };
        // var logMessage = $"LogHandleID: {logHandle.Id} assigned to : {ObjectId} ({ObjectType})";
        // logHandle.AttachLogMessage(configuration, LogLevel.None, logMessage);
        // loggingService.Log().Information(logMessage);
        await handleRepository.CreateAsync(logHandle);
        return logHandle;
    }
}