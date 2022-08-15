using CommonLibrary.Core;

namespace LogService.LogHandle;

public interface ILogHandle<T> where T : ILogMessage
{
    public Guid Id { get; set; }
    public Guid ObjectId { get; set; }
    public IIObject? Object{ get; set; }
    
    public Guid? SubjectId { get; set; }
    public string? LocationDetails { get; set; }
    public string? AuthorizationDetails { get; set; }
    public SortedSet<T> Messages { get; set; }
}