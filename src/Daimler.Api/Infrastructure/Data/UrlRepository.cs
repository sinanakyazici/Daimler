using System;
using Daimler.Api.Domains.Url;
using Daimler.Lib.Database;

namespace Daimler.Api.Infrastructure.Data
{
    public class UrlRepository : BaseRepository<Url>, IUrlRepository
    {
        public UrlRepository(IBaseContext<Url> context) : base(context)
        {
        }
    }
}