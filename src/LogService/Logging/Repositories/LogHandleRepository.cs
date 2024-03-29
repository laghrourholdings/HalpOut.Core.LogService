using System.Linq.Expressions;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using LogService.Core;
using Microsoft.EntityFrameworkCore;
using LogHandle = LogService.Logging.Models.LogHandle;

namespace LogService.Logging;

public class LogHandleRepository : IRepository<LogHandle>
{
    private readonly StoreDbContext _context;
    public LogHandleRepository(
        StoreDbContext context)
    {
        _context = context;
    }

    public async Task<List<LogHandle>?> GetAllAsync()
    {
        var logHandles = await _context.LogHandles.Include(x=>x.Messages).ToListAsync();
        return !logHandles.Any() ? null : logHandles;
    }

    public async Task<List<LogHandle>?> GetAllAsync(
        Expression<Func<LogHandle, bool>> filter)
    {
        var logHandles = await _context.LogHandles.Include(x=>x.Messages).Where(filter).ToListAsync();
        return !logHandles.Any() ? null : logHandles;

    }

    public async Task<LogHandle?> GetAsync(
        Guid Id)
    {
        var logHandle = await _context.LogHandles.Include(x=>x.Messages).SingleOrDefaultAsync(x => x.LogHandleId == Id);
        return logHandle ?? null;
    }

    public async Task<LogHandle?> GetAsync(
        Expression<Func<LogHandle, bool>> filter)
    {
        var logHandle = await _context.LogHandles.Include(x=>x.Messages).SingleOrDefaultAsync(filter);
        // var logMessages = await _messageRepository.GetAllAsync(x => x.LogHandleId == logHandle.Id);
        // if (logMessages != null)
        //     logHandle.Messages = logMessages.ToList();
        // else
        //     logHandle.Messages = null;
        return logHandle;
    }

    public async Task CreateAsync(
        LogHandle entity)
    {
        await _context.LogHandles.AddAsync(entity);
        await _context.SaveChangesAsync();
        // if (entity.Messages != null) 
        //     await _context.LogMessages.AddRangeAsync(entity.Messages);
        // await _context.SaveChangesAsync();
        //_loggingService.Verbose("LogHandle created",entity.LogHandleId);
    }

    public async Task RangeAsync(IEnumerable<LogHandle> logHandles)
    {
        await _context.AddRangeAsync(logHandles);
        await _context.SaveChangesAsync();
    }

    string GetMessage(
        ref LoggingInterpolatedStringHandler handler)
    {
        return handler.ToString();
    }

    public async Task UpdateAsync(
        LogHandle entity)
    {
        _context.LogHandles.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateOrCreateAsync(
        LogHandle entity)
    {
        LogHandle? obj = await _context.LogHandles.SingleOrDefaultAsync(x => x.LogHandleId == entity.LogHandleId);
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