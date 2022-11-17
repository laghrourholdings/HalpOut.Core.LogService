using CommonLibrary.Core;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace LogService.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class LogsController : ControllerBase
{
    private readonly IRepository<CommonLibrary.Logging.LogHandle> _handleRepository;
    
    public LogsController(IRepository<CommonLibrary.Logging.LogHandle> objectRepository, ILogger logger)
    {
        _handleRepository = objectRepository;
    }
    
    
    [HttpPost()]
    public async Task<IActionResult> CreateObject()
    {
        await _handleRepository.CreateAsync(null);
        return Ok("");
    }
 
    
    [HttpGet()]
    public async Task<IActionResult> GetAllHandles()
    {
        var objs = await _handleRepository.GetAllAsync();
        return Ok(objs);
    }
}