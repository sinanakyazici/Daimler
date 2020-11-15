using AutoMapper;
using Daimler.Api.Application.Commands.UrlCommands.AddUrl;
using Daimler.Api.Application.Commands.UrlCommands.UpdateUrl;
using Daimler.Api.Domains.Url;

namespace Daimler.Api.Application.Mappers
{
    public class UrlMappers : Profile
    {
        public UrlMappers()
        {
            CreateMap<AddUrlCommand, Url>();
        }
    }
}