using System.ComponentModel.DataAnnotations.Schema;

namespace CommonLibrary.Logging.Models;

 /// <summary>
    /// Default implementation for the ILogMessage BOI
/// </summary>
[Table("LogMessages")]
public class LogMessage //: ILogMessage, IEquatable<LogMessage>
{
    public Int64 Id { get; set; }
    public Int64 LogHandleId { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public LogLevel Severity { get; set; }
    public string? Message { get; set; }


    public bool Equals(LogMessage? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id) && LogHandleId.Equals(other.LogHandleId) && CreationDate.Equals(other.CreationDate) && Severity == other.Severity && Message == other.Message;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((LogMessage)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, LogHandleId, CreationDate, (int)Severity, Message);
    }
}
