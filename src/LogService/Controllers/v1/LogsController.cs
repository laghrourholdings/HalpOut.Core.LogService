using CommonLibrary.AspNetCore;
using CommonLibrary.Core;
using Microsoft.AspNetCore.Mvc;

namespace LogService.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class LogsController : ControllerBase
{
    private readonly IRepository<LogHandle.LogHandle> _handleRepository;
    
    public LogsController(IRepository<LogHandle.LogHandle> objectRepository)
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