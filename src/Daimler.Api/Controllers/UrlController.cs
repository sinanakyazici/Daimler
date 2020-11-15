using System.Threading.Tasks;
using Daimler.Api.Application.Commands.UrlCommands.AddUrl;
using Daimler.Api.Application.Commands.UrlCommands.UpdateUrl;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Daimler.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class UrlController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UrlController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("encode-url")]
        public async Task<IActionResult> EncodeUrlAsync(AddUrlCommand command)
        {
            var model = await _mediator.Send(command);
            return Ok(model);
        }

        [HttpPost]
        [Route("navigate")]
        public async Task<IActionResult> EncodeUrlAsync(UpdateUrlCommand command)
        {
            var model = await _mediator.Send(command);
            return Ok(model);
        }
    }
}