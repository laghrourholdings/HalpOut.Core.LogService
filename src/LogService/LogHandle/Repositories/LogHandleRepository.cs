using System.Linq.Expressions;
using CommonLibrary.Core;
using CommonLibrary.Logging;
using LogService.EFCore;
using Microsoft.EntityFrameworkCore;

namespace LogService.LogHandle;

public class LogHandleRepository : IRepository<CommonLibrary.Logging.LogHandle>
{
    private readonly ServiceDbContext _context;

    public LogHandleRepository(ServiceDbContext context)
    {
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

    string GetMessage(
        ref LoggingInterpolatedStringHandler handler)
    {
        return handler.ToString();
    }

    public async Task CreateAsync(CommonLibrary.Logging.LogHandle entity)
    {
        await _context.LogHandles.AddAsync(entity);
        await _context.SaveChangesAsync();
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
            _context.LogHandles.Update(obj);
        }
        await _context.SaveChangesAsync();    
    }
}