using System;
using Daimler.Api.Domains.Url;
using MediatR;

namespace Daimler.Api.Application.Commands.UrlCommands.AddUrl
{
    public class AddUrlCommand : IRequest<string>
    {
        public string UrlAddress { get; set; }
        public string Code { get; set; }
    }
}