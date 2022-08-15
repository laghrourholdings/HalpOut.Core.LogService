using System.ComponentModel.DataAnnotations.Schema;

namespace LogService.LogHandle;

public class LogMessage : ILogMessage
{
    public int Id { get; set; }
    public Guid LogHandleId { get; set; }
    [NotMapped]
    public ILogHandle<LogMessage> LogHandle { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public string Message { get; set; }
    public LogLevel Severity { get; set; }
}