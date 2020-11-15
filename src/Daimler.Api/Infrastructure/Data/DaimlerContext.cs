using Daimler.Api.Domains.Url;
using Daimler.Lib.Database;
using Daimler.Lib.Logger;
using Microsoft.AspNetCore.Http;

namespace Daimler.Api.Infrastructure.Data
{
    public class DaimlerContext : BaseContext<Url>
    {
        public DaimlerContext(IHttpContextAccessor httpContextAccessor, IDaimlerLogger daimlerLogger) : base(httpContextAccessor, daimlerLogger)
        {
        }
    }
}
