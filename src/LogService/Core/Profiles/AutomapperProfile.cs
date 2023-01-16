using AutoMapper;
using CommonLibrary.Logging.Models.Dtos;
using LogService.Logging.Models;

namespace LogService.Core;

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