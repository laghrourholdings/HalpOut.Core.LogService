using CommonLibrary.Core;
using CommonLibrary.Logging;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace LogService.Controllers.v1.Logs;

[Route("api/v{version:apiVersion}/[controller]/logs")]
[ApiVersion("1.0")]
[ApiController]
public class LogsController : ControllerBase
{
    private readonly IRepository<LogHandle> _handleRepository;
    private readonly IRepository<LogMessage> _messageRepository;
    private readonly ILogger _logger;

    public LogsController(
        IRepository<LogHandle> handleRepository,
        IRepository<LogMessage> messageRepository,
        ILogger logger)
    {
        _handleRepository = handleRepository;
        _messageRepository = messageRepository;
        _logger = logger;
    }

    

}