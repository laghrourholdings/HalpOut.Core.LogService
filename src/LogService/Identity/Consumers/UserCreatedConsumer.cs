using CommonLibrary.AspNetCore.Identity;
using CommonLibrary.AspNetCore.Logging;
using CommonLibrary.Core;
using LogService.Logging.Models;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace LogService.Identity.Consumers;

public class UserCreatedConsumer : IConsumer<UserCreated>
{
    
    private readonly ISecuromanService _securomanService;
    private readonly ILoggingService _loggingService;
    private readonly IDistributedCache _cache;

    public UserCreatedConsumer(
        ISecuromanService securomanService,
        ILoggingService loggingService,
        IConfiguration configuration)
    {
        _loggingService = loggingService;
        _securomanService = securomanService;
        _cache = Securoman.GetSecuromanCache(configuration);
    }

    
    public Task Consume(ConsumeContext<UserCreated> context)
    {
        var badge = context.Message.UserBadge;
        return _securomanService.SetOrUpdateUserAsync(badge);
    }
}