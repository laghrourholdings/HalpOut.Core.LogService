using Microsoft.AspNetCore.Mvc;

namespace LogService.Controllers.v1.Logs;

[Route("api/v{version:apiVersion}/[controller]/messages")]
[ApiVersion("1.0")]
public partial class LogsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllMessages()
    {
        var logMessages = await _messageRepository.GetAllAsync();
        return Ok(logMessages);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMessage(Guid Id)
    {
        var logMessages = await _messageRepository.GetAsync(Id);
        return Ok(logMessages);
    }
}