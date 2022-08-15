namespace LogService.LogHandle;

public interface ILogMessage
{
    public Int32 Id { get; set; }
    
    public Guid LogHandleId { get; set; }
    public ILogHandle<LogMessage> LogHandle { get; set; }
    
    public DateTimeOffset CreationDate { get; set; }
    public string Message { get; set; }
    public LogLevel Severity { get; set; }
}