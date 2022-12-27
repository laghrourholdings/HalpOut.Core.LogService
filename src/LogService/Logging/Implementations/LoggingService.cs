using CommonLibrary.AspNetCore.Logging;
using CommonLibrary.AspNetCore.Logging.LoggingService;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using MassTransit;

namespace LogService.Logging;
/// <summary>
/// Override of the default LoggingService implementation to allow logging to a database
/// The LoggingService must be used for all logging purposes.
/// </summary>
public class LoggingService : ILoggingService
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IConfiguration _config;
    private readonly Serilog.ILogger _logger;
    private readonly IRepository<LogMessage> _repository;
    // 
    public LoggingService(
        IPublishEndpoint publishEndpoint,
        IConfiguration config,
        Serilog.ILogger logger,
        IRepository<LogMessage> repository)
    {
        _publishEndpoint = publishEndpoint;
        _config = config;
        _logger = logger;
        _repository = repository;
    }
    public Serilog.ILogger Log()
    {
        return _logger;
    }
    
    /// <summary>
    /// Assigns a message for a provided logHandleId to the log message repository for creation. 
    /// </summary>
    /// <param name="message">Log message</param>
    /// <param name="logHandleId">Important: Must be provided if the message's target object has a logHandleId field</param>
    private void AssignMessage(string message,LogLevel severity, Guid logHandleId)
    {
        var logMessage = LogMessageExtentions.GetLogMessage(_config, severity, logHandleId, message);
        _repository.CreateAsync(logMessage);
    }
    
    //TODO Checks with cache to see if the logHandleId is valid
    /// <summary>
    /// Logs a debug message to configured sinks, and sends a copy to the database for archiving if a logHandleId is provided. 
    /// For debug types of messages, refrain from using the logHandleId in production. 
    /// </summary>
    /// <param name="message">Log message</param>
    /// <param name="logHandleId">Important: Must be provided if the message's target object has a logHandleId field</param>
    public void Debug(string message, Guid logHandleId = default)
    {
        _logger.Debug(message);
        if (logHandleId != Guid.Empty)
            AssignMessage(message,LogLevel.Debug, logHandleId);
    }

    /// <summary>
    /// Logs an information message to the configured sinks, and sends a copy to the database for archiving if a logHandleId is provided. 
    /// </summary>
    /// <param name="message">Log message</param>
    /// <param name="logHandleId">Important: Must be provided if the message's target object has a logHandleId field</param>
    public void Information(string message, Guid logHandleId)
    {
        _logger.Information(message);
        if(logHandleId != Guid.Empty)
            AssignMessage(message,LogLevel.Information, logHandleId);
    }

    /// <summary>
    /// Logs a verbose message to the configured sinks, and sends a copy to the database for archiving if a logHandleId is provided.
    /// </summary>
    /// <param name="message">Log message</param>
    /// <param name="logHandleId">Important: Must be provided if the message's target object has a logHandleId field</param>
    public void Verbose(string message, Guid logHandleId)
    {
        _logger.Verbose(message);
        if(logHandleId != Guid.Empty);
            AssignMessage(message,LogLevel.None, logHandleId);
    }

    /// <summary>
    /// Logs a critical message to the configured sinks, and sends a copy to the database for archiving if a logHandleId is provided.
    /// </summary>
    /// <param name="message">Log message</param>
    /// <param name="logHandleId">Important: Must be provided if the message's target object has a logHandleId field</param>
    public void Critical(string message, Guid logHandleId)
    {
        _logger.Fatal(message);
        if(logHandleId != Guid.Empty)
            AssignMessage(message,LogLevel.Critical, logHandleId);
    }

    /// <summary>
    /// Logs a warning message to the configured sinks, and sends a copy to the database for archiving if a logHandleId is provided.
    /// </summary>
    /// <param name="message">Log message</param>
    /// <param name="logHandleId">Important: Must be provided if the message's target object has a logHandleId field</param>
    public void Warning(string message, Guid logHandleId)
    {
        _logger.Warning(message);
        if(logHandleId != Guid.Empty)
            AssignMessage(message,LogLevel.Warning, logHandleId);
    }
    
    /// <summary>
    /// Logs an error message to the configured sinks, and sends a copy to the database for archiving if a logHandleId is provided.
    /// </summary>
    /// <param name="message">Log message</param>
    /// <param name="logHandleId">Important: Must be provided if the message's target object has a logHandleId field</param>
    public void Error(string message, Guid logHandleId)
    {
        _logger.Error(message);
        if(logHandleId != Guid.Empty)
            AssignMessage(message,LogLevel.Error, logHandleId);
    }
}