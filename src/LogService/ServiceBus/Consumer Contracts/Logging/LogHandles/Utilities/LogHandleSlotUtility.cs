using AutoMapper;
using CommonLibrary.AspNetCore.Logging.LoggingService;
using CommonLibrary.Core;
using CommonLibrary.Logging.Models.Dtos;
using LogHandle = LogService.Logging.Models.LogHandle;

namespace LogService.ServiceBus.Consumer_Contracts.Logging.LogHandles.Utilities;

public static class LogHandleSlotUtility
{
    /*public static async Task<LogHandle> GenerateLogHandleAsync(
        LogHandleDto logHandleDto,
        IMapper mapper,
        IConfiguration configuration,
        ILoggingService loggingService, 
        IRepository<LogHandle> handleRepository )
    {
        LogHandle logHandle = _;
        // var logMessage = $"LogHandleID: {logHandle.Id} assigned to : {ObjectId} ({ObjectType})";
        // logHandle.AttachLogMessage(configuration, LogLevel.None, logMessage);
        // loggingService.Log().Information(logMessage);
        await handleRepository.CreateAsync(logHandle);
        return logHandle;
    }*/
}