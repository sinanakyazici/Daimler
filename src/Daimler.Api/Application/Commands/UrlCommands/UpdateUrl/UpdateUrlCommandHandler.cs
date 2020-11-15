using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Daimler.Api.Domains.Url;
using MediatR;

namespace Daimler.Api.Application.Commands.UrlCommands.UpdateUrl
{
    public class UpdateUrlCommandHandler : IRequestHandler<UpdateUrlCommand, string>
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IMapper _mapper;

        public UpdateUrlCommandHandler(IUrlRepository urlRepository, IMapper mapper)
        {
            _urlRepository = urlRepository;
            _mapper = mapper;
        }
        public async Task<string> Handle(UpdateUrlCommand request, CancellationToken cancellationToken)
        {
            var url = _urlRepository.GetEntities().SingleOrDefault(x => x.UrlCode?.Code == request.Code);
            if (url != null)
            {
                url.RoutingCount++;
                _urlRepository.Update(url);
                return await Task.FromResult(url.UrlAddress);
            }

            return await Task.FromResult(ClientUrl.BaseUri.ToString());
        }
    }
}