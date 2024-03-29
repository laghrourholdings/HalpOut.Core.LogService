using System.ComponentModel.DataAnnotations;
using CommonLibrary.Logging.Models.Dtos;

namespace LogService.Logging.Models;

/// <summary>
/// The LogHandle class is the default implementation of the ILogHandle
/// The LogMessage is used as the type parameter for ILogMessage
/// </summary>

public class LogHandle //: ILogHandle<LogMessage,List<LogMessage>>, IEquatable<LogHandle>
{
    [Key]
    public Int64 Id { get; set; }
    public Guid LogHandleId { get; set; }
    public Guid ObjectId { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsSuspended { get; set; }
    public DateTimeOffset DeletedDate { get; set; }
    public Guid DeletedBy { get; set; }
    public DateTimeOffset SuspendedDate { get; set; }
    public Guid SuspendedBy { get; set; }
    public string ObjectType { get; set; }
    public string? LocationDetails { get; set; }
    public string? AuthorizationDetails { get; set; }
    public string? Descriptor { get; set; }

    public ICollection<LogMessage> Messages { get; set; } = new HashSet<LogMessage>();

    public bool Equals(LogHandle? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return LogHandleId.Equals(other.LogHandleId) && ObjectId.Equals(other.ObjectId);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((LogHandle)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(LogHandleId, ObjectId);
    }

    public static LogHandle FromDto(LogHandleDto logHandleDto)
    {
        return new LogHandle()
        {
            LogHandleId = logHandleDto.LogHandleId,
            ObjectId = logHandleDto.ObjectId,
            ObjectType = logHandleDto.ObjectType,
            LocationDetails = logHandleDto.LocationDetails,
            AuthorizationDetails = logHandleDto.AuthorizationDetails,
            Descriptor = logHandleDto.Descriptor,
            CreationDate = logHandleDto.CreationDate
        };
    }
}
