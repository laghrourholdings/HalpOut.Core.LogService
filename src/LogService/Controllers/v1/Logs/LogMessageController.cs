using CommonLibrary.Core;
using CommonLibrary.Logging;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace LogService.Controllers.v1.Logs;

[Route("api/v{version:apiVersion}/Logs/messages")]
[ApiVersion("1.0")]
public class LogsMessagesController : ControllerBase
{
    private readonly IRepository<LogHandle> _handleRepository;
    private readonly IRepository<LogMessage> _messageRepository;
    private readonly ILogger _logger;

    public LogsMessagesController(
        IRepository<LogHandle> handleRepository,
        IRepository<LogMessage> messageRepository,
        ILogger logger)
    {
        _handleRepository = handleRepository;
        _messageRepository = messageRepository;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllMessages()
    {
        var logMessages = await _messageRepository.GetAllAsync();
        return Ok(logMessages);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMessage(Guid Id)
    {
        var logMessages = await _messageRepository.GetAsync(Id);
        return Ok(logMessages);
    }
}