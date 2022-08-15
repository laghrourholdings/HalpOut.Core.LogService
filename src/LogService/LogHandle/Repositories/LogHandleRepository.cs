using System.Linq.Expressions;
using CommonLibrary.AspNetCore.Logging;
using CommonLibrary.AspNetCore.ServiceBus;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using LogService.EFCore;
using Microsoft.EntityFrameworkCore;

namespace LogService.LogHandle;

public class LogHandleRepository : IRepository<LogHandle>
{
    private readonly ServiceDbContext _context;

    public LogHandleRepository(ServiceDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<LogHandle>> GetAllAsync()
    {
        return await _context.LogHandles.ToListAsync();
    }

    public Task<IEnumerable<LogHandle>> GetAllAsync(
        Expression<Func<LogHandle, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public Task<LogHandle> GetAsync(
        Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<LogHandle> GetAsync(
        Expression<Func<LogHandle, bool>> filter)
    {
        throw new NotImplementedException();
    }

    string GetMessage(
        ref LoggingInterpolatedStringHandler handler)
    {
        return handler.ToString();
    }

    public async Task CreateAsync(LogHandle entity)
    {
        await _context.LogHandles.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    
    public Task UpdateAsync(
        LogHandle entity)
    {
        throw new NotImplementedException();
    }
}