using System.Linq.Expressions;
using CommonLibrary.AspNetCore.Logging.LoggingService;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using LogService.EFCore;
using Microsoft.EntityFrameworkCore;
using ILogger = Serilog.ILogger;

namespace LogService.Logs;

public class LogHandleRepository : IRepository<LogHandle>
{
    private readonly ServiceDbContext _context;
    private readonly IRepository<LogMessage> _messageRepository;
    private readonly ILoggingService _loggingService;

    public LogHandleRepository(
        IRepository<LogMessage> messageRepository,
        ServiceDbContext context, ILoggingService loggingService)
    {
        _messageRepository = messageRepository;
        _loggingService = loggingService;
        _context = context;
    }

    public async Task<IEnumerable<LogHandle>?> GetAllAsync()
    {
        var logHandles = await _context.LogHandles.ToListAsync();
        if (logHandles.Count == 0)
        {
            return null;
        }
        foreach (var logHandle in logHandles)
        {
            var logMessages = await _messageRepository.GetAllAsync(x => x.LogHandleId == logHandle.Id);
            if (logMessages != null)
                logHandle.Messages = logMessages.ToList();
            else
                logHandle.Messages = null;
        }
        return logHandles;
    }

    public async Task<IEnumerable<LogHandle>?> GetAllAsync(
        Expression<Func<LogHandle, bool>> filter)
    {
        var logHandles = await _context.LogHandles.Where(filter).ToListAsync();
        if (logHandles.Count == 0)
        {
            return null;
        }
        foreach (var logHandle in logHandles)
        {
            var logMessages = await _messageRepository.GetAllAsync(x => x.LogHandleId == logHandle.Id);
            if (logMessages != null)
                logHandle.Messages = logMessages.ToList();
            else
                logHandle.Messages = null;
        }
        return logHandles;
    }

    public async Task<LogHandle?> GetAsync(
        Guid Id)
    {
        var logHandle = await _context.LogHandles.SingleOrDefaultAsync(x => x.Id == Id);
        if (logHandle == null)
            return null;
        var logMessages = await _messageRepository.GetAllAsync(x => x.LogHandleId == logHandle.Id);
        if (logMessages != null)
            logHandle.Messages = logMessages.ToList();
        else
            logHandle.Messages = null;
        return logHandle;
    }

    public async Task<LogHandle?> GetAsync(
        Expression<Func<LogHandle, bool>> filter)
    {
        var logHandle = await _context.LogHandles.SingleOrDefaultAsync(filter);
        if (logHandle == null)
            return null;
        var logMessages = await _messageRepository.GetAllAsync(x => x.LogHandleId == logHandle.Id);
        if (logMessages != null)
            logHandle.Messages = logMessages.ToList();
        else
            logHandle.Messages = null;
        return logHandle;
    }

    public async Task CreateAsync(
        LogHandle entity)
    {
        await _context.LogHandles.AddAsync(entity);
        if (entity.Messages != null) 
            await _context.LogMessages.AddRangeAsync(entity.Messages);
        await _context.SaveChangesAsync();
        _loggingService.Verbose("LogHandle created",entity.Id);
    }

    public async Task RangeAsync(IEnumerable<LogHandle> logHandles)
    {
        await _context.AddRangeAsync(logHandles);
        foreach (var logHandle in logHandles)
        {
            if (logHandle.Messages != null) await _context.LogMessages.AddRangeAsync(logHandle.Messages);
        }
    }

    string GetMessage(
        ref LoggingInterpolatedStringHandler handler)
    {
        return handler.ToString();
    }

    public Task UpdateAsync(
        LogHandle entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateOrCreateAsync(
        LogHandle entity)
    {
        LogHandle? obj = await _context.LogHandles.SingleOrDefaultAsync(x => x.Id == entity.Id);
        if (obj is null)
        {
            try
            {
                await CreateAsync(entity);
            }catch(Exception ex)
            {
                await UpdateOrCreateAsync(entity);
                return;
            }
        }
        else
        {
            _context.LogHandles.Update(entity);
        }
        await _context.SaveChangesAsync();    
    }
}