using System.Linq.Expressions;
using CommonLibrary.AspNetCore.Logging.LoggingService;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using CommonLibrary.Logging.Models;
using LogService.EFCore;
using Microsoft.EntityFrameworkCore;

namespace LogService.Logging;

public class LogMessageRepository : IRepository<LogMessage>
{
    private readonly ServiceDbContext _context;
   // private readonly ILoggingService _loggingService;
    //private readonly IRepository<CommonLibrary.Logging.LogHandle> _handleRepository;

    public LogMessageRepository(
        ServiceDbContext context)
    {
        //_loggingService = loggingService;
        _context = context;
        //_handleRepository = handleRepository;
    }


    public async Task<IEnumerable<LogMessage>?> GetAllAsync()
    {
        var logMessages = await _context.LogMessages.ToListAsync();
        return logMessages.Count == 0 ? null : logMessages;
    }

    public async Task<IEnumerable<LogMessage>?> GetAllAsync(Expression<Func<LogMessage, bool>> filter)
    {
        var logMessages = await _context.LogMessages.Where(filter).ToListAsync();
        return logMessages.Count == 0 ? null : logMessages;
    }

    public async Task<LogMessage?> GetAsync(Guid Id)
    {
        return await _context.LogMessages.SingleOrDefaultAsync(x => x.Id == Id);
    }

    public async Task<LogMessage?> GetAsync(Expression<Func<LogMessage, bool>> filter)
    {
        return await _context.LogMessages.SingleOrDefaultAsync(filter);
    }

    public async Task CreateAsync(LogMessage logMessage)
    {
        var existantLogMessage = await GetAsync(logMessage.Id);
        if (existantLogMessage != null)
        {
            logMessage.Id = Guid.NewGuid();
            logMessage.Descriptor = logMessage.Descriptor?.Insert(0,"[Id already exists - duplicate] ");
        }
        await _context.LogMessages.AddAsync(logMessage);
        await _context.SaveChangesAsync();
    }

    public async Task RangeAsync(IEnumerable<LogMessage> logMessages)
    {
        var logMessagesList = logMessages.ToList();
        await _context.AddRangeAsync(logMessagesList);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(LogMessage logMessage)
    {
        _context.Update(logMessage);
        await _context.SaveChangesAsync();
    }

    public Task UpdateOrCreateAsync(LogMessage logMessage)
    {
        throw new NotImplementedException();
    }
}