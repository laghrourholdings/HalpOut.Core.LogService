using AutoMapper;
using CommonLibrary.AspNetCore.Logging;
using CommonLibrary.Core;
using CommonLibrary.Logging.Models;
using CommonLibrary.Logging.Models.Dtos;
using LogService.Logging.Models;
using Microsoft.AspNetCore.Mvc;
using LogHandle = LogService.Logging.Models.LogHandle;

namespace LogService.Controllers.v1.Logs;

[Route("api/v{version:apiVersion}/logs/handles")]
[ApiVersion("1.0")]
public class LogHandlesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRepository<LogHandle> _handleRepository;
    private readonly ILoggingService _loggingService;

    public LogHandlesController(
        IMapper mapper,
        IRepository<LogHandle> handleRepository,
        ILoggingService loggingService)
    {
        _mapper = mapper;
        _handleRepository = handleRepository;
        _loggingService = loggingService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllHandles()
    {
        var logHandles = await _handleRepository.GetAllAsync();
        if (logHandles is not null)
        {
            var logHandlesDto = logHandles.Select(x => _mapper.Map<LogHandle, LogHandleDto>(x));
            return Ok(logHandlesDto);
        }
        return Ok();
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetHandle(Guid Id)
    {
        var logHandle = await _handleRepository.GetAsync(x=>x.LogHandleId == Id);
        return Ok(logHandle);
    }
}