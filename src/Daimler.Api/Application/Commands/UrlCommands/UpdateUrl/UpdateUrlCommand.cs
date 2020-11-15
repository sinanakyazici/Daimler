using MediatR;

namespace Daimler.Api.Application.Commands.UrlCommands.UpdateUrl
{
    public class UpdateUrlCommand : IRequest<string>
    {
        public string Code { get; set; }
    }
}