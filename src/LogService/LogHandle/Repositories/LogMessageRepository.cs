using System.Linq.Expressions;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using LogService.EFCore;
using Microsoft.EntityFrameworkCore;
using ILogger = Serilog.ILogger;

namespace LogService.LogHandle;

public class LogMessageRepository : IRepository<LogMessage>
{
    private readonly ServiceDbContext _context;
    private readonly ILogger _logger;
    private readonly IRepository<CommonLibrary.Logging.LogHandle> _handleRepository;

    public LogMessageRepository(
        ServiceDbContext context,
        ILogger logger,
        IRepository<CommonLibrary.Logging.LogHandle> handleRepository)
    {
        _logger = logger;
        _context = context;
        _handleRepository = handleRepository;
    }


    public Task<IEnumerable<LogMessage>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LogMessage>> GetAllAsync(Expression<Func<LogMessage, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public async Task<LogMessage?> GetAsync(Guid Id)
    {
        return await _context.LogMessages.SingleOrDefaultAsync(x => x.Id == Id);
    }

    public Task<LogMessage?> GetAsync(Expression<Func<LogMessage, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public async Task CreateAsync(LogMessage logMessage)
    {
        var existantLogMessage = await GetAsync(logMessage.Id);
        if (existantLogMessage != null)
        {
            logMessage.Id = Guid.NewGuid();
            logMessage.Descriptor = logMessage.Descriptor?.Insert(0,"[Id already exists - duplicate] ");
        }
        _logger.Information("LogMessage created: {Entity}", logMessage);
        await _context.LogMessages.AddAsync(logMessage);
        await _context.SaveChangesAsync();
    }

    public Task RangeAsync(IEnumerable<LogMessage> entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(LogMessage logMessage)
    {
        throw new NotImplementedException();
    }

    public Task UpdateOrCreateAsync(LogMessage logMessage)
    {
        throw new NotImplementedException();
    }
}