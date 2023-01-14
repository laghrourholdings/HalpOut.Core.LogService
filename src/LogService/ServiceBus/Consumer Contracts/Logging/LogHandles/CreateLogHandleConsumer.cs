using AutoMapper;
using CommonLibrary.AspNetCore.Logging.LoggingService;
using CommonLibrary.AspNetCore.ServiceBus.Contracts.Logging;
using CommonLibrary.Core;
using CommonLibrary.Logging.Models.Dtos;
using LogService.ServiceBus.Consumer_Contracts.Logging.LogHandles.Utilities;
using MassTransit;
using LogHandle = LogService.Logging.Models.LogHandle;

namespace LogService.ServiceBus.Consumer_Contracts.Logging.LogHandles;

public class CreateLogHandleConsumer : IConsumer<CreateLogHandle>
{
    private readonly IMapper _mapper;
    private readonly IRepository<LogHandle> _handleRepository;
    private readonly ILoggingService _loggingService;
    private readonly IConfiguration _configuration;

    public CreateLogHandleConsumer(
        IMapper mapper,
        IRepository<LogHandle> handleRepository,
        ILoggingService loggingService,
        IConfiguration configuration)
    {
        _mapper = mapper;
        _handleRepository = handleRepository;
        _loggingService = loggingService;
        _configuration = configuration;
    }

    
    public async Task Consume(ConsumeContext<CreateLogHandle> context)
    {
        var logHandleDto = context.Message.LogHandleDto;
        var logHandle = _mapper.Map<LogHandleDto,LogHandle>(logHandleDto);
        await _handleRepository.CreateAsync(logHandle);
        // await LogHandleSlotUtility.GenerateLogHandleAsync(logHandleDto.Id,
        //     logHandleDto.ObjectId, logHandleDto.ObjectType,
        //     _configuration, _loggingService, _handleRepository);
    }
}