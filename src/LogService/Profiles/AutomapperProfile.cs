using AutoMapper;
using CommonLibrary.AspNetCore.ServiceBus;
using CommonLibrary.Logging.Models;
using CommonLibrary.Logging.Models.Dtos;
using LogService.Logging.Models;

namespace LogService.Profiles;

public class LogModelsProfile : Profile
{
    public LogModelsProfile()
    {
        //Source -> Target
        CreateMap<LogHandleDto, LogHandle>();
        CreateMap<LogMessageDto, LogMessage>();
        CreateMap<LogHandle, LogHandleDto>();
        CreateMap<LogMessage, LogMessageDto>();
    }
}