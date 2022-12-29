using CommonLibrary.AspNetCore.Logging.LoggingService;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using CommonLibrary.Logging.Models;
using Microsoft.AspNetCore.Mvc;

namespace LogService.Controllers.v1.Logs;

[Route("api/v{version:apiVersion}/[controller]/logs")]
[ApiVersion("1.0")]
[ApiController]
public class LogsController : ControllerBase
{
    private readonly IRepository<LogHandle> _handleRepository;
    private readonly IRepository<LogMessage> _messageRepository;
    private readonly ILoggingService _loggingService;

    public LogsController(
        IRepository<LogHandle> handleRepository,
        IRepository<LogMessage> messageRepository,
        ILoggingService loggingService)
    {
        _handleRepository = handleRepository;
        _messageRepository = messageRepository;
        _loggingService = loggingService;
    }

    

}