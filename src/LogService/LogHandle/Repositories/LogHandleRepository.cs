using System.Linq.Expressions;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using LogService.EFCore;
using Microsoft.EntityFrameworkCore;
using ILogger = Serilog.ILogger;

namespace LogService.LogHandle;

public class LogHandleRepository : IRepository<CommonLibrary.Logging.LogHandle>
{
    private readonly ServiceDbContext _context;
    private readonly ILogger _logger;

    public LogHandleRepository(ServiceDbContext context, ILogger logger)
    {
        _logger = logger;
        _context = context;
    }
    
    public async Task<IEnumerable<CommonLibrary.Logging.LogHandle>> GetAllAsync()
    {
        return await _context.LogHandles.ToListAsync();
    }

    public Task<IEnumerable<CommonLibrary.Logging.LogHandle>> GetAllAsync(
        Expression<Func<CommonLibrary.Logging.LogHandle, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public Task<CommonLibrary.Logging.LogHandle> GetAsync(
        Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<CommonLibrary.Logging.LogHandle> GetAsync(
        Expression<Func<CommonLibrary.Logging.LogHandle, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public async Task CreateAsync(
        CommonLibrary.Logging.LogHandle entity)
    {
        _logger.Information("LogHandle created: {Entity}", entity);
        await _context.LogHandles.AddAsync(entity);
        if (entity.Messages != null) 
            await _context.LogMessages.AddRangeAsync(entity.Messages);
        await _context.SaveChangesAsync();
    }

    string GetMessage(
        ref LoggingInterpolatedStringHandler handler)
    {
        return handler.ToString();
    }

    public Task UpdateAsync(
        CommonLibrary.Logging.LogHandle entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateOrCreateAsync(
        CommonLibrary.Logging.LogHandle entity)
    {
        CommonLibrary.Logging.LogHandle? obj = await _context.LogHandles.SingleOrDefaultAsync(x => x.Id == entity.Id);
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