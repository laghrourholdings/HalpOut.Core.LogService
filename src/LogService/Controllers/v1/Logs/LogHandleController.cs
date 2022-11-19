using CommonLibrary.Logging;
using Microsoft.AspNetCore.Mvc;

namespace LogService.Controllers.v1.Logs;

[Route("api/v{version:apiVersion}/[controller]/handles")]
[ApiVersion("1.0")]
public partial class LogsController : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAllHandles()
    {
        var logHandles = await _handleRepository.GetAllAsync();
        return Ok(logHandles);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllLogs()
    {
        var logHandles = await _handleRepository.GetAllAsync();
        return Ok(logHandles);
    }
    [HttpGet]
    public async Task<IActionResult> GetHandle(Guid Id)
    {
        var logHandle = await _handleRepository.GetAsync(Id);
        return Ok(logHandle);
    }
}