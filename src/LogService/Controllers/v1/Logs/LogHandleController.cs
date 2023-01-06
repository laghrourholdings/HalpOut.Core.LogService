﻿using CommonLibrary.AspNetCore.Logging.LoggingService;
using CommonLibrary.Core;
using CommonLibrary.Logging.Models;
using Microsoft.AspNetCore.Mvc;

namespace LogService.Controllers.v1.Logs;

[Route("api/v{version:apiVersion}/logs/handles")]
[ApiVersion("1.0")]
public class LogHandlesController : ControllerBase
{
    private readonly IRepository<LogHandle> _handleRepository;
    private readonly IRepository<LogMessage> _messageRepository;
    private readonly ILoggingService _loggingService;

    public LogHandlesController(
        IRepository<LogHandle> handleRepository,
        IRepository<LogMessage> messageRepository,
        ILoggingService loggingService)
    {
        _handleRepository = handleRepository;
        _messageRepository = messageRepository;
        _loggingService = loggingService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllHandles()
    {
        var logHandles = await _handleRepository.GetAllAsync();
        return Ok(logHandles);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetHandle(Guid Id)
    {
        var logHandle = await _handleRepository.GetAsync(Id);
        return Ok(logHandle);
    }
}