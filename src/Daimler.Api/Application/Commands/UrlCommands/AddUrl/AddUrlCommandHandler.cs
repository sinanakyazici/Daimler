using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Daimler.Api.Domains.Url;
using Daimler.Lib.Helpers;
using MediatR;

namespace Daimler.Api.Application.Commands.UrlCommands.AddUrl
{
    public class AddUrlCommandHandler : IRequestHandler<AddUrlCommand, string>
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IMapper _mapper;

        public AddUrlCommandHandler(IUrlRepository urlRepository, IMapper mapper)
        {
            _urlRepository = urlRepository;
            _mapper = mapper;
        }
        public async Task<string> Handle(AddUrlCommand request, CancellationToken cancellationToken)
        {
            var result = Uri.TryCreate(request.UrlAddress, UriKind.Absolute, out var uriResult)
                          && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            if (!result) throw new Exception("Invalid Uri");
            if (!string.IsNullOrWhiteSpace(request.Code) && _urlRepository.GetEntities().Any(x => x.UrlCode?.Code == request.Code)) throw new Exception("Used Code");
            var url = _mapper.Map<Url>(request);
            _urlRepository.Add(url);
            var code = !string.IsNullOrWhiteSpace(request.Code) ? request.Code : Helper.GetCode(url.Id); // uretilen kod unique oldugu icin var mi yokmu diye bakmaya gerek yok.
            url.UrlCode = new UrlCode { UrlId = url.Id, Code = code };
            _urlRepository.Update(url);
            // var code2 = Helper.GenerateCode(6);
            return await Task.FromResult(new Uri(ClientUrl.BaseUri, url.UrlCode.Code).ToString());
        }
    }
}